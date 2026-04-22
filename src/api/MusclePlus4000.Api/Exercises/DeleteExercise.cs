using MediatR;
using MusclePlus4000.Api.Abstractions;
using MusclePlus4000.Application.Exercises.Commands.DeleteExercise;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Api.Exercises;

public sealed class DeleteExercise : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/exercises/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteExerciseCommand(id), cancellationToken);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.NoContent();
            })
            .WithName("DeleteExercise")
            .WithTags(Tags.Exercises)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status404NotFound);
    }
}

