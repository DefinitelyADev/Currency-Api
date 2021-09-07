using System;
using System.Collections.Generic;
using System.Reflection;
using CurrencyApi.Application.Interfaces;
using CurrencyApi.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApi.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyModelBuilderConfigurations(this ModelBuilder modelBuilder)
        {
            Singleton<ITypeFinder>.Instance ??= new WebAppTypeFinder();

            IEnumerable<Type> entityConfigurations =
                Singleton<ITypeFinder>.Instance.FindClassesOfType(typeof(IEntityTypeConfiguration<>));

            // Get ApplyConfiguration method with reflection
            MethodInfo? applyGenericMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);

            foreach (Type entityConfiguration in entityConfigurations)
            {
                // Use type.Namespace to filter by namespace if necessary
                foreach (Type iface in entityConfiguration.GetInterfaces())
                {
                    // Make concrete ApplyConfiguration<SomeEntity> method
                    MethodInfo? applyConcreteMethod = applyGenericMethod?.MakeGenericMethod(iface.GenericTypeArguments[0]);
                    // Invoke that with fresh instance of your configuration type
                    applyConcreteMethod?.Invoke(modelBuilder, new[] { Activator.CreateInstance(entityConfiguration) });
                }
            }
        }
    }
}