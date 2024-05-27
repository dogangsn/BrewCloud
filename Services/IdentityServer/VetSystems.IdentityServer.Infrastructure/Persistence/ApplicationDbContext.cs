using Dapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using VetSystems.IdentityServer.Infrastructure.Entities;

namespace VetSystems.IdentityServer.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<SubscriptionAccount> SubscriptionAccounts { get; set; }
        public DbSet<SubscriptionSafeList> SubscriptionSafeList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Accounts>().HasOne((x) => x.User).WithOne((x) => x.Account).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public int Execute(string query, object parameters)
        {
            return Database.GetDbConnection().Execute(query, parameters);
        }
        public List<T> SQLQuery<T>(string query, object parameters)
        {

            return Database.GetDbConnection().Query<T>(query, param: parameters).ToList();
        }
        public List<T> SQLQuery<T>(string query)
        {
            return Database.GetDbConnection().Query<T>(query).ToList();
        }

    }
}
