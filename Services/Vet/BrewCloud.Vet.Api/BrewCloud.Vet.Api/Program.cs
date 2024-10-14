    using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BrewCloud.Vet.Application;
using BrewCloud.Vet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_vet";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
    options.RequireHttpsMetadata = false;
});
builder.Services.AddControllers();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddOptions();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
