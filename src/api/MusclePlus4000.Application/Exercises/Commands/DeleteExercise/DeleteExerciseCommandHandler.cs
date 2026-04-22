using MediatR;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Application.Exercises.Commands.DeleteExercise;

public sealed class DeleteExerciseCommandHandler(IExerciseWriteRepository exerciseWriteRepository)
    : IRequestHandler<DeleteExerciseCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var exercise = await exerciseWriteRepository.GetByIdAsync(request.Id, cancellationToken);

        if (exercise is null)
            return Error.NotFound("Exercise.NotFound", $"Exercise with id '{request.Id}' was not found.");

        exerciseWriteRepository.Remove(exercise);
        await exerciseWriteRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}

