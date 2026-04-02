namespace MusclePlus4000.Application.Common.Interfaces;

/// <summary>
/// Abstraction over the database context used by application-layer handlers.
/// Infrastructure implements this; Application depends only on the interface.
/// </summary>
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}


