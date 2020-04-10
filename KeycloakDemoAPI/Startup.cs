using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeycloakDemoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration["Jwt:Authority"];
                o.Audience = Configuration["Jwt:Audience"];
                o.RequireHttpsMetadata = false;

                bool isDevelopment =
                    "development".Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower());
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(isDevelopment ? c.Exception.ToString() : "An error occured during authentication.");
                    }
                };
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorsInspectionMiddleware>();
            app.UseCors(//"AllowAll"
                options => options.WithOrigins("http://keycloak-demo.ngrok.io",
                    "https://keycloak-demo.ngrok.io", "http://localhost:5000",
                    "https://keycloak-demo-ui.ngrok.io", "http://keycloak-demo-ui.ngrok.io",
                    "https://keycloak-demo-api.ngrok.io", "http://keycloak-demo-api.ngrok.io",
                    "https://localhost:4321", "https://intranet-dev-usz.virtualcorp.ch/").AllowAnyMethod()
                // options.AllowAnyOrigin().AllowAnyMethod()
                .WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization")
                .WithExposedHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization").AllowCredentials()
            );
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}