using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Accounts>().HasOne((x) => x.User).WithOne((x) => x.Account).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

    }
}
