using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using HotelBooking.Api.Constants;
using HotelBooking.Api.Extensions;
using HotelBooking.Api.Middlewares;
using HotelBooking.Application.Extensions.DependencyInjection;
using HotelBooking.Infrastructure.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Host.UseSerilog((context, config) =>
    config
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration));

services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

services.BindConfigurations(builder.Configuration);
services.AddControllers();
services.AddSwaggerUi();
services.AddEndpointsApiExplorer();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddInfrastructure();
services.AddDomainServices(builder.Configuration);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddRateLimitingService();
services.ConfigureApiVersioning();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseRateLimiter();
app
    .MapControllers()
    .RequireRateLimiting(RateLimitingPolicies.FixedWindowPolicy);

app.Run();
