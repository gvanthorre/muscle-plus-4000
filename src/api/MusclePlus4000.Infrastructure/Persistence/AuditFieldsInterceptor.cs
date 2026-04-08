using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Infrastructure.Persistence;

public sealed class AuditFieldsInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    private const string SystemActor = "System";

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAuditFields(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }

        var now = timeProvider.GetUtcNow().UtcDateTime;

        foreach (EntityEntry<AuditableEntity> entry in dbContext.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CreatedAt == default || string.IsNullOrWhiteSpace(entry.Entity.CreatedBy))
                {
                    entry.Entity.SetCreated(SystemActor, now);
                }

                entry.Entity.SetModified(SystemActor, now);
                continue;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetModified(SystemActor, now);
            }
        }
    }
}

