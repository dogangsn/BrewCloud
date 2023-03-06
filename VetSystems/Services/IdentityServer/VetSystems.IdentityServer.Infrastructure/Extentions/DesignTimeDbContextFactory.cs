﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.IdentityServer.Infrastructure.Persistence;

namespace VetSystems.IdentityServer.Infrastructure.Extentions
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //string connectionString = _configuration.GetConnectionString("DefaultConnection");
            dbContextOptionsBuilder.UseSqlServer("Server=DG1;Database=VetAdminIdentityDb;User Id=sa;Password=123654Dg;");
            return new ApplicationDbContext(dbContextOptionsBuilder.Options);

        }
    }
}
