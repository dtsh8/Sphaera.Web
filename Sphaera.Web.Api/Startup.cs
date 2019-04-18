using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sphaera.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["IdentityServer"];
                    options.RequireHttpsMetadata = true;
                    options.Audience = "api1";
                });

            services.AddCors(options =>
            {
                options.AddPolicy("ClientsOnly", policy =>
                {
                    policy.WithOrigins(Configuration["JSClient_Uri"], "https://localhost:44334", "https://localhost:44301")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("ClientsOnly");
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
