using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace BrewCloud.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowCors",
                           builder =>
                           {
                               builder
                                .AllowAnyOrigin()
                               .WithMethods("GET", "PUT", "POST", "DELETE")
                               .AllowAnyHeader();
                           }));
            services.AddCors(options => options.AddPolicy("allowLocal", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    //.WithOrigins("https://*.veboni.com").SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));
            services.AddOcelot();
        }
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowCors");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            app.UseWebSockets();
            await app.UseOcelot();
        }
    }
}
