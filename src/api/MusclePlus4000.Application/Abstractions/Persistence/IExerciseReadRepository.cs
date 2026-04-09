using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

namespace MusclePlus4000.Application.Abstractions.Persistence;

public interface IExerciseReadRepository
{
    Task<IReadOnlyList<ExerciseListItemDto>> GetAllAsync(CancellationToken cancellationToken);
}

