using VetSystems.Gateway;

public class Program
{
    public static void Main(string[] args)
    {

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
           })
           .ConfigureWebHostDefaults(webBuilder =>
           {
                   //webBuilder.UseUrls("http://localhost:5010", "http://192.168.40.134:5010");
               webBuilder.UseStartup<Startup>();
           });
    
}