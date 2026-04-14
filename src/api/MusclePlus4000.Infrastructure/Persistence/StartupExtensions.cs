using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MusclePlus4000.Infrastructure.Persistence;

public static class StartupExtensions
{
    public static async Task VerifyDatabaseConnectionAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<WorkoutDbContext>>();
        try
        {
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.CloseConnectionAsync();
            logger.LogInformation("Database connection successful");
        }
        catch (Exception)
        {
            logger.LogError("Database connection failed");
            throw;
        }
    }
}

