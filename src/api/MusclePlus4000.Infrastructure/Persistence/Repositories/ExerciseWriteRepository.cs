using Microsoft.EntityFrameworkCore;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Domain.Entities;

namespace MusclePlus4000.Infrastructure.Persistence.Repositories;

public sealed class ExerciseWriteRepository(WorkoutDbContext dbContext) : IExerciseWriteRepository
{
    public async Task AddAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await dbContext.Exercises.AddAsync(exercise, cancellationToken);
    }

    public async Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Exercises.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Remove(Exercise exercise)
    {
        dbContext.Exercises.Remove(exercise);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}

