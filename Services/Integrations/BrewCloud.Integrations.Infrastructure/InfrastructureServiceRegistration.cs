using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Integrations.Domain.Contracts;
using BrewCloud.Integrations.Infrastructure.Persistence;
using BrewCloud.Integrations.Infrastructure.Repositories;
using BrewCloud.Shared.Service;

namespace BrewCloud.Integrations.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddDbContext<VetDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
