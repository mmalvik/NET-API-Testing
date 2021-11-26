using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Commenting out UseSerilog makes test output in xUnit work
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(hostingContext.Configuration);
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Net6WepApp", Version = "v1" }); });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net6WepApp v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}