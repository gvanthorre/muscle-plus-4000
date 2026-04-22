using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;
using MusclePlus4000.Application.Exercises.Queries.GetExerciseById;

namespace MusclePlus4000.Application.Abstractions.Persistence;

public interface IExerciseReadRepository
{
    Task<IReadOnlyList<ExerciseListItemDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ExerciseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

