using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VetSystems.IdentityServer.Infrastructure.Persistence
{
    public class PersistedGrantDataContext : PersistedGrantDbContext
    {
        public PersistedGrantDataContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Bunu manuel oluşturmak zorunda kaldım
            //Postgre den kaynaklıbir sebebten dolayı kendiliğinden oluşmadı ve burada Designtimefactory kullandım
            Console.WriteLine("OnModelCreating invoking...");

            base.OnModelCreating(modelBuilder);

            // Map the entities to different tables here
            //modelBuilder.Entity<IdentityServer4.EntityFramework.Entities.ApiResource>().ToTable("mytesttable");

            Console.WriteLine("...OnModelCreating invoked");
        }
    }

    public class PersistedGrantDataDesignTimeFactory : IDesignTimeDbContextFactory<PersistedGrantDataContext>
    {
        public PersistedGrantDataContext CreateDbContext(string[] args)
        {
            // var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();
            // var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbContextBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();

            dbContextBuilder.UseSqlServer("Server=DG1;Database=VetAdminIdentityDb;User Id=sa;Password=123654Dg;", postGresOptions => postGresOptions.MigrationsAssembly(typeof(PersistedGrantDataDesignTimeFactory).GetTypeInfo().Assembly.GetName().Name));
            // DbContextOptions<ConfigurationDbContext> ops = dbContextBuilder.Options;

            // dbContextBuilder.UseSqlServer(connectionString);

            return new PersistedGrantDataContext(dbContextBuilder.Options, new OperationalStoreOptions());

            //  throw new NotImplementedException();
        }
    }
}

