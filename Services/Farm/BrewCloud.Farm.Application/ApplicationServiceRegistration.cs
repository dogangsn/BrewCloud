using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using BrewCloud.Shared.Service;
using BrewCloud.Shared.Accounts;
using Identity.Api;
using BrewCloud.Farm.Application.GrpServices;

namespace BrewCloud.Farm.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddHttpContextAccessor();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddScoped<ExceptionHandlingMiddleware>();
            //services.Configure<DatabaseSettings>(configuration["GrpcSettings:IdentityUrl"]);

            services.AddGrpcClient<IdentityUserProtoService.IdentityUserProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:IdentityUrl"]));

            services.AddScoped<IdentityGrpService>();
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });
            //services.AddMassTransit(config =>
            //{
            //    config.AddConsumer<UpdateDatabaseConsumer>();
            //    config.UsingRabbitMq((ctx, cfg) =>
            //    {
            //        cfg.Host(configuration["EventBusSettings:HostAddress"]);
            //        cfg.ReceiveEndpoint(EventBusConstants.MigrateDatabaseQueueErp,
            //            c => { c.ConfigureConsumer<UpdateDatabaseConsumer>(ctx); });
            //    });
            //});

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("ConnectionString") + " Trust Server Certificate=true;");
            return services;
        }
    }
}
