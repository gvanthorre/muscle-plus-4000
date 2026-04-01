using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MusclePlus4000.Api.Infrastructure.Persistence;

public class WorkoutDbContext(
    DbContextOptions<WorkoutDbContext> options
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}