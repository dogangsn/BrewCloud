using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BrewCloud.Account.Infrastructure
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BrewCloud.Account/BrewCloud.Account.Api"));
                configurationManager.AddJsonFile("appsettings.json");
                configurationManager.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                configurationManager.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
                return configurationManager.GetConnectionString("ConnectionString");
            }
        }




    }
}
