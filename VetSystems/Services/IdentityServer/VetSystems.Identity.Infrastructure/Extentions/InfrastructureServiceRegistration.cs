
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VetSystems.Identity.Infrastructure.Entities;
using VetSystems.Identity.Infrastructure.Persistence;
using VetSystems.Identity.Infrastructure.Repositories;
using VetSystems.Identity.Infrastructure.Services;
using VetSystems.Identity.Infrastructure.Services.Interface;

namespace VetSystems.Identity.Infrastructure.Extentions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string migrationsAssembly = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<ConfigurationDataContext>(options =>
             options.UseSqlServer(connectionString).UseLowerCaseNamingConvention());

            services.AddDbContext<PersistedGrantDataContext>(options =>
           options.UseSqlServer(connectionString).UseLowerCaseNamingConvention());


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddTransient<IAccountService, AccountService>();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential();
            builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<PasswordValidatorService>();
            builder.AddExtensionGrantValidator<GrantValidator>();

            services.AddTransient<IProfileService, AdminProfileService>();
            return services;
        }
    }
}
