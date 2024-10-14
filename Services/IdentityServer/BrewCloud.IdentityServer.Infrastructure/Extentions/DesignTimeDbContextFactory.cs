using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.IdentityServer.Infrastructure.Persistence;

namespace BrewCloud.IdentityServer.Infrastructure.Extentions
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");
            dbContextOptionsBuilder.UseSqlServer("Server=vetsytemsdb,1433;Database=VetAdminIdentityDb;User Id=sa;Password=123654Dg;");
            return new ApplicationDbContext(dbContextOptionsBuilder.Options);

        }
    }
}
