using DataLayer;
using Microsoft.EntityFrameworkCore;
using NetWebApi.Extensions;
using NetWebApi.Middleware;
using NetWebApi.Repositories;
using NetWebApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(hostingContext.Configuration);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "NetWebApi", Version = "v1" });
});

builder.Services.AddDbContext<WeatherForecastContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherForecasts"));
});

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

var app = builder.Build();

app.UseDatabase<WeatherForecastContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net6WepApp v1"));
}

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
