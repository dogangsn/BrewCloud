using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using BrewCloud.Account.Application;
using BrewCloud.Account.Application.Behaviours;
using BrewCloud.Account.Infrastructure;
using BrewCloud.Shared.Accounts;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<TenantDbSettings>(builder.Configuration.GetSection("TemantDbSettings"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_account";
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseResponseCompression();
app.UseMiddleware<ExceptionHandlingMiddleware>();
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
