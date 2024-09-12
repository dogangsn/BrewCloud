using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VetSystems.Hubs.Api.Features;

namespace VetSystems.Hubs.Api.Extentions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<RefreshAppointmentCalendarCommand>();

            return services;
        }
    }
}
