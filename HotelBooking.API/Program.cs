using System.Reflection;
using FluentValidation;
using HotelBooking.Application.Extensions.DependencyInjection;
using HotelBooking.Db.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

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
