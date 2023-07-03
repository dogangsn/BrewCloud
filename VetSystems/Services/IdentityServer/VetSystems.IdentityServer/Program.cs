// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;
using System.Net;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Persistence;
using VetSystems.Shared.Common;

namespace VetSystems.IdentityServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            //    .MinimumLevel.Override("System", LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    // uncomment to write to Azure diagnostics stream
            //    //.WriteTo.File(
            //    //    @"D:\home\LogFiles\Application\identityserver.txt",
            //    //    fileSizeLimitBytes: 1_000_000,
            //    //    rollOnFileSizeLimit: true,
            //    //    shared: true,
            //    //    flushToDiskInterval: TimeSpan.FromSeconds(1))
            //    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
            //    .CreateLogger();

            try
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
                var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables();

                var config = builder.Build();
                //var host = CreateHostBuilder(args).Build();
                var host = CreateHostBuilder(config, args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var appDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    appDbContext.Database.Migrate();

                    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    if (!userManager.Users.Any())
                    {
                        userManager.CreateAsync(new ApplicationUser { UserName = "Deneme", Email = "deneme@deneme.com" }, "123D654Dg!").Wait();
                    }
                }

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .UseSerilog()
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });


        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>

          Host.CreateDefaultBuilder(args)
              //.UseSerilog(SeriLogger.Configure)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.ConfigureKestrel(options =>
                  {

                      //TODO: Enverment a göre etirilecek
                      var ports = GetDefinedPorts(configuration);

                      options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
                      {
                          listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                      });
                      options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
                      {
                          listenOptions.Protocols = HttpProtocols.Http2;
                      });
                      // options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2);

                  });
                  webBuilder.UseStartup<Startup>();
              });

        static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var grpcPort = config.GetValue("GRPC_PORT", 5009);
            var port = config.GetValue("PORT", 5001);
            return (port, grpcPort);
        }
    }
}