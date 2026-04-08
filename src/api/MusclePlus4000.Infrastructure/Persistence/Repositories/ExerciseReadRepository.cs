using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

namespace MusclePlus4000.Infrastructure.Persistence.Repositories;

public sealed class ExerciseReadRepository(WorkoutDbContext dbContext) : IExerciseReadRepository
{
    public async Task<IReadOnlyList<ExerciseListItemDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Exercises
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new ExerciseListItemDto(x.Id, x.Name, x.Description))
            .ToListAsync(cancellationToken);
    }
}

