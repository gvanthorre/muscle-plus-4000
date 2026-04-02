using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Application.Common.Interfaces;

namespace MusclePlus4000.Infrastructure.Persistence;

public class WorkoutDbContext(
    DbContextOptions<WorkoutDbContext> options
) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}

