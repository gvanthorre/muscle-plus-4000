using MediatR;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Application.Exercises.Commands.DeleteExercise;

public sealed record DeleteExerciseCommand(Guid Id) : IRequest<Result<bool>>;

