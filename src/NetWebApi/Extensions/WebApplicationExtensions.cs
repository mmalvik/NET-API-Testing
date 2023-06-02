using Microsoft.EntityFrameworkCore;

namespace NetWebApi.Extensions;

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
                if (context.Database.GetPendingMigrations().Any())
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