using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Application.Service;
using BrewCloud.Mail.Domain.Entities;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;

namespace BrewCloud.Mail.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IIdentityRepository, IdentityRepository>();

            services.AddHttpClient("auth", c =>
            {
                c.BaseAddress = new Uri(configuration["IdentityServerURL"]);
            });

            //services.AddGrpcClient<IdentityUserProtoService.IdentityUserProtoServiceClient>
            // (o => o.Address = new Uri(configuration["GrpcSettings:IdentityUrl"]));
            //services.AddScoped<IdentityGrpService>();

            services.Configure<SmtpSetting>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();


            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("ConnectionString") + " Trust Server Certificate=true;");

            return services;
        }
    }
}
