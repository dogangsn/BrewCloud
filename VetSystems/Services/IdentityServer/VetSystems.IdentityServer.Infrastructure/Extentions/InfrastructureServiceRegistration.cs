using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Persistence;

namespace VetSystems.IdentityServer.Infrastructure.Extentions
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
          .AddInMemoryIdentityResources(Config.IdentityResources)
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryClients(Config.Clients)
          .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });

            return services;
        }



    }
}
