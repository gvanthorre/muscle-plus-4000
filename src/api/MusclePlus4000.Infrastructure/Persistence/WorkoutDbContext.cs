using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Domain.Common;
using MusclePlus4000.Domain.Entities;

namespace MusclePlus4000.Infrastructure.Persistence;

public class WorkoutDbContext(
    DbContextOptions<WorkoutDbContext> options
) : DbContext(options)
{
    public DbSet<Exercise> Exercises => Set<Exercise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("app");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ConfigureAuditableEntities(modelBuilder);

        var seedTimestamp = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Exercise>().HasData(
            new
            {
                Id = Guid.Parse("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0001"),
                Name = "Bench Press",
                Description = "Lie on a bench and push weights",
                CreatedAt = seedTimestamp,
                CreatedBy = "System",
                LastModifiedAt = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = Guid.Parse("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0002"),
                Name = "Back Squat",
                Description = "Take weights on your shoulders, go down then up",
                CreatedAt = seedTimestamp,
                CreatedBy = "System",
                LastModifiedAt = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = Guid.Parse("018f2a3e-4f7a-7b24-b78f-8d1ec2dc0003"),
                Name = "Pull Up",
                Description = "Grab a bar above yourself and get up",
                CreatedAt = seedTimestamp,
                CreatedBy = "System",
                LastModifiedAt = (DateTime?)null,
                LastModifiedBy = (string?)null
            });

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureAuditableEntities(ModelBuilder modelBuilder)
    {
        var auditableTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(t => typeof(AuditableEntity).IsAssignableFrom(t.ClrType));

        foreach (var entityType in auditableTypes)
        {
            var builder = modelBuilder.Entity(entityType.ClrType);

            builder.Property(nameof(AuditableEntity.CreatedAt))
                .IsRequired();

            builder.Property(nameof(AuditableEntity.CreatedBy))
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(nameof(AuditableEntity.LastModifiedAt))
                .IsRequired(false);

            builder.Property(nameof(AuditableEntity.LastModifiedBy))
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}
