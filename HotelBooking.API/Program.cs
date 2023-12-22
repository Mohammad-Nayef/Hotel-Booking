using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
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
services.AddSwaggerGen(setup =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setup.IncludeXmlComments(xmlCommentsFullPath);
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddDatabase();
services.AddDomainServices();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
