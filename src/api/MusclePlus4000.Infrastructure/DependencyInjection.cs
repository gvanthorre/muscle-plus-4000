using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Infrastructure.Persistence;
using MusclePlus4000.Infrastructure.Persistence.Repositories;

namespace MusclePlus4000.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddScoped<AuditFieldsInterceptor>();
        services.AddScoped<IExerciseReadRepository, ExerciseReadRepository>();

        services.AddDbContext<WorkoutDbContext>((serviceProvider, dbContextOptions) =>
            dbContextOptions
                .UseNpgsql(configuration["ConnectionStrings:Default"],
                    o => o
                        .MigrationsAssembly("MusclePlus4000.Infrastructure")
                        .MigrationsHistoryTable("__EFMigrationsHistory", "app"))
                .AddInterceptors(serviceProvider.GetRequiredService<AuditFieldsInterceptor>()));

        return services;
    }
}

