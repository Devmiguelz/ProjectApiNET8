using FluentValidation;
using FluentValidation.AspNetCore;
using PruebaAnnarApi.Application.DependencyInjection;
using PruebaAnnarApi.Domain.DependencyInjection;
using PruebaAnnarApi.Infrastructure.DependencyInjection;
using PruebaAnnarApi.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(config);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
}).AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
