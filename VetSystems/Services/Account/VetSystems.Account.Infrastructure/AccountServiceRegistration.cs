using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VetSystemsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.Configure<TenantDbSettings>(configuration.GetSection("TemantDbSettings"));
            //services.AddSingleton<ITenantDbSettings>(sp =>
            //{
            //    return sp.GetRequiredService<TenantDbSettings>();
            //});


        }
    }
}
