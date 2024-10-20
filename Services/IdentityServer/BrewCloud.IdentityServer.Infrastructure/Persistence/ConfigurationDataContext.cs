﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BrewCloud.IdentityServer.Infrastructure.Persistence
{
    public class ConfigurationDataContext : ConfigurationDbContext
    {
        public ConfigurationDataContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Console.WriteLine("OnModelCreating invoking...");

            base.OnModelCreating(modelBuilder);

            // Map the entities to different tables here
            //modelBuilder.Entity<IdentityServer4.EntityFramework.Entities.ApiResource>().ToTable("mytesttable");

            Console.WriteLine("...OnModelCreating invoked");
        }
    }

    public class ConfigurationDataDesignTimeFactory : IDesignTimeDbContextFactory<ConfigurationDataContext>
    {
        public ConfigurationDataContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile($"appsettings.{env}.json")
           .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var dbContextBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();

            dbContextBuilder.UseSqlServer(connectionString, postGresOptions => postGresOptions.MigrationsAssembly(typeof(ConfigurationDataDesignTimeFactory).GetTypeInfo().Assembly.GetName().Name));
            // DbContextOptions<ConfigurationDbContext> ops = dbContextBuilder.Options;

            // dbContextBuilder.UseSqlServer(connectionString);

            return new ConfigurationDataContext(dbContextBuilder.Options, new ConfigurationStoreOptions());

            //  throw new NotImplementedException();
        }
    }
}

