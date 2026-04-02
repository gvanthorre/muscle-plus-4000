using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusclePlus4000.Application.Common.Interfaces;
using MusclePlus4000.Infrastructure.Persistence;

namespace MusclePlus4000.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<WorkoutDbContext>(options =>
            options.UseNpgsql(
                configuration["ConnectionStrings:Default"],
                o => o.MigrationsHistoryTable("__EFMigrationsHistory", "app")));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<WorkoutDbContext>());

        return services;
    }

    /// <summary>
    /// Opens and immediately closes a connection to verify the database is reachable.
    /// Call once at startup from Program.cs.
    /// </summary>
    public static async Task ValidateDatabaseConnectionAsync(
        this IServiceProvider services,
        ILogger logger)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
        try
        {
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.CloseConnectionAsync();
            logger.LogInformation("Database connection successful");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database connection failed");
            throw;
        }
    }
}

