using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Domain.Contracts;
using BrewCloud.Mail.Infrastructure.Persistance;
using BrewCloud.Mail.Infrastructure.Repositories;
using BrewCloud.Shared.Service;

namespace BrewCloud.Mail.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddDbContext<MailDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
