using MediatR;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Domain.Common;
using MusclePlus4000.Domain.Entities;

namespace MusclePlus4000.Application.Exercises.Commands.CreateExercise;

public sealed class CreateExerciseCommandHandler(IExerciseWriteRepository exerciseWriteRepository)
    : IRequestHandler<CreateExerciseCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateExerciseCommand request,
        CancellationToken cancellationToken)
    {
        var result = Exercise.Create(request.Name, request.Description, request.CreatedBy);

        if (result.IsFailure)
            return result.Error;

        await exerciseWriteRepository.AddAsync(result.Value, cancellationToken);
        await exerciseWriteRepository.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}

