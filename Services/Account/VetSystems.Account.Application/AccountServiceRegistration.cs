using Identity.Api;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Application.Attributes;
using VetSystems.Account.Application.Behaviours;
using VetSystems.Account.Application.Features.Consumers;
using VetSystems.Account.Application.Features.Settings.Queries;
using VetSystems.Account.Application.GrpServices;
using VetSystems.Shared.Common;
using VetSystems.Shared.Service;
using VetSystems.Shared.Service.Nav;

namespace VetSystems.Account.Application
{
    public static class AccountServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
 
            });
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddGrpcClient<IdentityUserProtoService.IdentityUserProtoServiceClient>
                    (o => o.Address = new Uri(configuration["GrpcSettings:IdentityUrl"]));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IdentityGrpService>();
            services.AddSingleton<ModuleService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                var loggerFactory = container.GetRequiredService<ILoggerFactory>();
                var identityRepository = container.GetRequiredService<IIdentityRepository>();
                var identityGrpService = container.GetRequiredService<IdentityGrpService>();
                var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();
                var mediator = container.GetRequiredService<IMediator>();
                return new ClientIpCheckActionFilter(configuration["AdminSafeList"], logger, identityRepository,
                    identityGrpService);
            });
            services.AddScoped<ExceptionHandlingMiddleware>();

            services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateAccountConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.MigrateDatabaseQueue,
                        c => { c.ConfigureConsumer<CreateAccountConsumer>(ctx); });
                     
                });
            });

            services.AddHealthChecks();
              //.AddSqlServer(configuration.GetConnectionString("ConnectionString") + " Trust Server Certificate=true;", "SELECT 1;", null);


            return services;


        }
    }
}
