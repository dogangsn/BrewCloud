using Identity.Api;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Account.Commands;
using VetSystems.Vet.Application.GrpServices;
using VetSystems.Vet.Application.Services.Mails;

namespace VetSystems.Vet.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddGrpcClient<IdentityUserProtoService.IdentityUserProtoServiceClient>
                (o => o.Address = new Uri(configuration["GrpcSettings:IdentityUrl"]));
      
            services.AddScoped<IdentityGrpService>();
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddHttpClient("mail", c =>
            {
                c.BaseAddress = new Uri(configuration["ApiGatewayUrl"]);
            });
            services.AddSingleton<IMailService, MailService>();

            services.AddMassTransit(config =>
            {
                //config.UsingRabbitMq((ctx, cfg) =>
                //{
                //    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                //    //cfg.UseHealthCheck(ctx);
                //});
            });
            services.AddMassTransitHostedService();

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("ConnectionString") + " Trust Server Certificate=true;");
            return services;
        }
    }
}
