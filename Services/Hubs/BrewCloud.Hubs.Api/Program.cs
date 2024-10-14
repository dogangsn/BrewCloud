using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BrewCloud.Hubs.Api.Extentions;
using BrewCloud.Hubs.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSignalR();


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                       policy.AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials()
                             .SetIsOriginAllowed(origin => true)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ServiceHub>("/serviceHub");
});


app.MapGet("/", () => "Hello World!");

app.Run();
