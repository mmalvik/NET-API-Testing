using Net6WebApi.Middleware;
using Net6WebApi.Repositories;
using Net6WebApi.Services;
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
    c.SwaggerDoc("v1", new() { Title = "Net6WepApp", Version = "v1" });
});


builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

var app = builder.Build();

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
