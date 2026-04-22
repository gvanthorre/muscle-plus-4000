using MediatR;
using MusclePlus4000.Application.Abstractions.Persistence;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Application.Exercises.Queries.GetExerciseById;

public sealed class GetExerciseByIdQueryHandler(IExerciseReadRepository exerciseReadRepository)
    : IRequestHandler<GetExerciseByIdQuery, Result<ExerciseDto>>
{
    public async Task<Result<ExerciseDto>> Handle(
        GetExerciseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var exercise = await exerciseReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (exercise is null)
            return Error.NotFound("Exercise.NotFound", $"Exercise with id '{request.Id}' was not found.");

        return exercise;
    }
}

