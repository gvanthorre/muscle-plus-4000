using MediatR;
using MusclePlus4000.Application.Abstractions.Persistence;

namespace MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

public sealed class GetAllExercisesQueryHandler(IExerciseReadRepository exerciseReadRepository)
    : IRequestHandler<GetAllExercisesQuery, IReadOnlyList<ExerciseListItemDto>>
{
    public Task<IReadOnlyList<ExerciseListItemDto>> Handle(
        GetAllExercisesQuery request,
        CancellationToken cancellationToken)
    {
        return exerciseReadRepository.GetAllAsync(cancellationToken);
    }
}

