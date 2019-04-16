// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;

using IdentityServer4;

using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Sphaera.IdentityServer;

namespace IdentityServer
{
    public class Startup
    {
        public const string PfxPwd = "123456";
        public IHostingEnvironment Environment { get; }
        public static IContainer Container { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            //});

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    options.SignIn.RequireConfirmedEmail = false;
            //    options.SignIn.RequireConfirmedPhoneNumber = true;
            //})
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();



            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());

            //services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseErrorEvents = true;
            //    options.Events.RaiseInformationEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseSuccessEvents = true;
            //    options.IssuerUri = Configuration["IdmBaseAddress"];
            //})
            //.AddOperationalStore<ApplicationDbContext>()
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            //.AddInMemoryApiResources(Config.GetApiResources())
            //.AddInMemoryClients(Config.GetClients())
            //.AddAspNetIdentity<ApplicationUser>()
            //.AddSigningCredential(new X509Certificate2(Path.Combine(".", "Cert", "cert.pfx"), PfxPwd))
            //.AddProfileService<UserProfileProvider>();

            if (Environment.IsDevelopment())
                builder.AddDeveloperSigningCredential();
            else
                throw new Exception("need to configure key material");

            services.AddMvc()
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //services.AddIdentityServer(o =>
            //{
            //    o.Events.RaiseErrorEvents = true;
            //    o.Events.RaiseInformationEvents = true;
            //    o.Events.RaiseFailureEvents = true;
            //    o.Events.RaiseSuccessEvents = true;
            //    o.IssuerUri = Configuration["IdmBaseAddress"];
            //})
            //.AddOperationalStore<ApplicationDbContext>()
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            //.AddInMemoryClients(Config.GetClients())
            //.AddAspNetIdentity<ApplicationUser>()
            //.AddSigningCredential(new X509Certificate2(Path.Combine(".", "Cert", "cert.pfx"), PfxPwd))
            //.AddProfileService<UserProfileProvider>();
            

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterModule<ModuleProduction>();

            services.AddAuthentication()
                .AddOpenIdConnect("oidc", "OpenID Connect", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            Container = containerBuilder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}