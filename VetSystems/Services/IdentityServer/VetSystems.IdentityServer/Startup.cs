// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using VetSystems.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetSystems.IdentityServer.Infrastructure.Persistence;
using VetSystems.IdentityServer.Infrastructure.Extentions;
using VetSystems.IdentityServer.Application.Extentions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Http;
using VetSystems.IdentityServer.Grpc;

namespace VetSystems.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalApiAuthentication();
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddGrpc();

            services.AddCors(options => options.AddPolicy("AllowCors",
                 builder =>
                 {
                     builder
                     .AllowAnyOrigin()
                      // .WithOrigins(dm.ToArray())
                      .WithMethods("GET", "PUT", "POST", "DELETE")
                      .AllowAnyHeader();
                 }));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddInfrastructureServices(Configuration);
            services.AddApplicationServices(Configuration);

        }

        public void Configure(IApplicationBuilder app, ApplicationDbContext appDbContext)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<IdentityUserGrpService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });


            DatabaseInitializer.Initialize(app, appDbContext);
        }
    }
}