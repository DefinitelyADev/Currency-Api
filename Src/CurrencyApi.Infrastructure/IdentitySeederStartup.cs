﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyApi.Application.Interfaces.Core;
using CurrencyApi.Domain.Entities;
using CurrencyApi.Infrastructure.Core.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi.Infrastructure
{
    public class IdentityInitializer : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

        public void Configure(IApplicationBuilder application)
        {
            IEngine engine = EngineContext.Current;
            var userManager = engine.Resolve<UserManager<User>>();

            if (userManager == null)
                throw new InvalidOperationException("User manager cannot be null during the seeding process.");

            User? user = userManager.FindByNameAsync("admin").Result;

            if (user != null)
                return;

            RoleManager<IdentityRole>? roleManager = engine.Resolve<RoleManager<IdentityRole>>();

            if (roleManager == null)
                throw new InvalidOperationException("Role manager cannot be null during the seeding process.");

            roleManager.CreateAsync(new IdentityRole("admin")).Wait();
            roleManager.CreateAsync(new IdentityRole("user")).Wait();

            userManager.CreateAsync(new User("admin"), "defaultAdminPass").Wait();
            User? adminUser = userManager.FindByNameAsync("admin").Result;
            userManager.AddToRoleAsync(adminUser, "admin");

            userManager.CreateAsync(new User("user"), "defaultUserPass").Wait();
            User? simpleUser = userManager.FindByNameAsync("user").Result;
            userManager.AddToRoleAsync(simpleUser, "user");
        }

        public int Order => 200;
    }
}