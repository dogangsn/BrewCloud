using Identity.Api;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using BrewCloud.Chat.Application.GrpServices;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;

namespace BrewCloud.Chat.Api.Configuration
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

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("ConnectionString") + " Trust Server Certificate=true;");
            return services;
        }
    }
}
