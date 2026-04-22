using MusclePlus4000.Domain.Entities;

namespace MusclePlus4000.Application.Abstractions.Persistence;

public interface IExerciseWriteRepository
{
    Task AddAsync(Exercise exercise, CancellationToken cancellationToken);
    Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Remove(Exercise exercise);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

