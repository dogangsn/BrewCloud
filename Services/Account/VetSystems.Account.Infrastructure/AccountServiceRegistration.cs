using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Infrastructure.Persistence;
using VetSystems.Account.Infrastructure.Repositories;
using VetSystems.Shared.Accounts;

namespace VetSystems.Account.Infrastructure
{
    public static class AccountServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<Shared.Service.ITenantRepository, TenantRepository>();
            services.AddScoped<Shared.Service.IIdentityRepository, Shared.Service.IdentityRepository>();
            services.AddDbContext<VetSystemsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
 
            return services;

        }
    }
}
