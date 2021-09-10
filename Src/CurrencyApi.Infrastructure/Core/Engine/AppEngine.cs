using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Interfaces.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure.Core.Engine
{
    /// <summary>
    /// Represents App engine
    /// </summary>
    public sealed class AppEngine : IEngine
    {
        #region Properties

        /// <summary>
        /// Gets or sets service provider
        /// </summary>
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "If the violation is fixed it causes a conflict with the ServiceProvider class")]
        private IServiceProvider? _serviceProvider { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Service provider
        /// </summary>
        public IServiceProvider? ServiceProvider => _serviceProvider;

        #endregion

        #region Utilities

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        private IServiceProvider? GetServiceProvider()
        {
            if (ServiceProvider == null)
            {
                return null;
            }

            IHttpContextAccessor? accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            HttpContext? context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// Run startup tasks
        /// </summary>
        /// <param name="typeFinder">Type finder</param>
        private void RunStartupTasks(ITypeFinder typeFinder)
        {
            //find startup tasks provided by other assemblies
            IEnumerable<Type> startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            //create and sort instances of startup tasks
            //we startup this interface even for not installed plugins.
            //otherwise, DbContext initializers won't run and a plugin installation won't work
            IOrderedEnumerable<IStartupTask?> instances = startupTasks
                .Select(startupTask => (IStartupTask?)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask?.Order);

            //execute tasks
            foreach (IStartupTask? task in instances)
            {
                task?.Execute();
            }
        }

        private Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            Assembly? assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
            {
                return assembly;
            }

            //get assembly from TypeFinder
            ITypeFinder? tf = Resolve<ITypeFinder>();

            if (tf == null)
            {
                return null;
            }

            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add and configure services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = new WebAppTypeFinder();

            //find startup configurations provided by other assemblies
            IEnumerable<Type> startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            //create and sort instances of startup configurations
            IOrderedEnumerable<IAppStartup?> startupInstances = startupConfigurations
                .Select(startup => (IAppStartup?)Activator.CreateInstance(startup))
                .OrderBy(startup => startup?.Order);

            //configure services
            foreach (IAppStartup? instance in startupInstances)
            {
                instance?.ConfigureServices(services, configuration);
            }

            //run startup tasks
            RunStartupTasks(typeFinder);

            //resolve assemblies here. otherwise, plugins can throw an exception when rendering views
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            //register engine
            services.AddSingleton<IEngine>(this);

            //register type finder
            services.AddSingleton(typeFinder);

            //find dependency registrars provided by other assemblies
            IEnumerable<Type> dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            IOrderedEnumerable<IDependencyRegistrar?> dependencyRegistrarInstances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar?)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar?.Order);

            //register all provided dependencies
            foreach (IDependencyRegistrar? dependencyRegistrar in dependencyRegistrarInstances)
                dependencyRegistrar?.Register(services, typeFinder);

            services.AddSingleton(services);
        }

        /// <summary>
        /// Configure HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            _serviceProvider = application.ApplicationServices;

            //find startup configurations provided by other assemblies
            ITypeFinder typeFinder = new WebAppTypeFinder();

            IEnumerable<Type> startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            //create and sort instances of startup configurations
            IOrderedEnumerable<IAppStartup?> instances = startupConfigurations
                .Select(startup => (IAppStartup?)Activator.CreateInstance(startup))
                .OrderBy(startup => startup?.Order);

            //configure request pipeline
            foreach (IAppStartup? instance in instances)
                instance?.Configure(application);
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T? Resolve<T>() where T : class
        {
            return (T?)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object? Resolve(Type type)
        {
            IServiceProvider? sp = GetServiceProvider();
            return sp?.GetService(type);
        }

        /// <summary>
        /// Resolve required dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        /// <exception cref="InvalidOperationException">Throws an invalid operation exception if the service provider is null or the requested service could not be found.</exception>
        public T ResolveRequired<T>() where T : class
        {
            return (T)ResolveRequired(typeof(T));
        }

        /// <summary>
        /// Resolve required dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        /// <exception cref="InvalidOperationException">Throws an invalid operation exception if the service provider is null or the requested service could not be found.</exception>
        public object ResolveRequired(Type type)
        {
            IServiceProvider? sp = GetServiceProvider();

            if (sp == null)
                throw new InvalidOperationException("Cannot resolve service from a null service provider.");

            return sp.GetRequiredService(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public IEnumerable<T>? ResolveAll<T>() => (IEnumerable<T>?)GetServiceProvider()?.GetServices(typeof(T));

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns>Resolved service</returns>
        public object? ResolveUnregistered(Type type)
        {
            WebAppException? innerException = null;
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    IEnumerable<object> parameters = constructor.GetParameters().Select(parameter =>
                    {
                        object? service = Resolve(parameter.ParameterType);

                        if (service == null)
                        {
                            throw new WebAppException("Unknown dependency");
                        }

                        return service;
                    });

                    //everything is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (WebAppException ex)
                {
                    innerException = ex;
                }
            }

            throw new WebAppException("No constructor was found that had all the dependencies satisfied.", innerException!);
        }

        #endregion
    }
}
