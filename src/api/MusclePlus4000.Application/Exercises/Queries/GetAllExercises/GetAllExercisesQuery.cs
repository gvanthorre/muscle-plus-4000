using MediatR;

namespace MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

public sealed record GetAllExercisesQuery : IRequest<IReadOnlyList<ExerciseListItemDto>>;

