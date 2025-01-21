using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static void UseDatabase<TContext>(this WebApplication app) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TContext>();
            
            // Create the database if it does not exist
            if (context.Database.EnsureCreated())
            {
                if (Enumerable.Any<string>(context.Database.GetPendingMigrations()))
                {
                    context.Database.Migrate();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<TContext>>();
            logger.LogError(ex, "An error occurred while migrating or initializing the database");
        }
    }
}