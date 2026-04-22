using MediatR;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(string Name, string Description, string CreatedBy)
    : IRequest<Result<Guid>>;

