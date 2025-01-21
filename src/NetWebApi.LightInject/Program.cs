using Api.Shared.Extensions;
using Api.Shared.Middleware;
using Api.Shared.Repositories.Repositories;
using Api.Shared.Services.Services;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NetWebApi.LightInject;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Host.UseLightInject(services => services.RegisterFrom<CompositionRoot>());

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

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

var app = builder.Build();

app.UseDatabase<WeatherForecastContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetWebApi LightInject v1"));
}

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();