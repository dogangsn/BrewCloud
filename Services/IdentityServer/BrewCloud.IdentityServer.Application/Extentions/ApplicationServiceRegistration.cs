
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BrewCloud.IdentityServer.Application.Services;
using BrewCloud.Shared.Service;

namespace BrewCloud.IdentityServer.Application.Extentions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    //cfg.UseHealthCheck(ctx);
                });
            });
            services.AddHttpClient("account", c =>
            {
                c.BaseAddress = new Uri(configuration["ApiGatewayUrl"]);
            });
            services.AddSingleton<IAccountDataService, AccountDataService>();
            services.AddMassTransitHostedService();
            services.AddHealthChecks();
                //.AddSqlServer(configuration.GetConnectionString("DefaultConnection"), "SELECT 1;", null);


            return services;

        }
    }
}
