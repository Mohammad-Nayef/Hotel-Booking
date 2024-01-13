using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using HotelBooking.Api.Extensions;
using HotelBooking.Application.Extensions.DependencyInjection;
using HotelBooking.Db.Extensions.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

services.AddControllers();
services.AddSwaggerUi();
services.AddEndpointsApiExplorer();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddDatabase();
services.AddDomainServices(builder.Configuration);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
