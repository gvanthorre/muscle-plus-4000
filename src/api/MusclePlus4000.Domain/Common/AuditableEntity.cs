namespace MusclePlus4000.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? LastModifiedAt { get; private set; }
    public string? LastModifiedBy { get; private set; }

    public void SetCreated(string createdBy, DateTime? timestamp = null)
    {
        CreatedBy = createdBy;
        CreatedAt = timestamp ?? DateTime.UtcNow;
    }

    public void SetModified(string modifiedBy, DateTime? timestamp = null)
    {
        LastModifiedBy = modifiedBy;
        LastModifiedAt = timestamp ?? DateTime.UtcNow;
    }
}