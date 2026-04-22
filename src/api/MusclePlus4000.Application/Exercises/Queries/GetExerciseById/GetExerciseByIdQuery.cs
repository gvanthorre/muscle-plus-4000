using MediatR;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Application.Exercises.Queries.GetExerciseById;

public sealed record GetExerciseByIdQuery(Guid Id) : IRequest<Result<ExerciseDto>>;

