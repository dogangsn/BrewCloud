using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Infrastructure.Persistence;
using BrewCloud.Account.Infrastructure.Repositories;
using BrewCloud.Shared.Accounts;

namespace BrewCloud.Account.Infrastructure
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
            services.AddDbContext<BrewCloudDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
 
            return services;

        }
    }
}
