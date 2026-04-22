using MediatR;
using MusclePlus4000.Api.Abstractions;
using MusclePlus4000.Application.Exercises.Commands.CreateExercise;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Api.Exercises;

public sealed class CreateExercise : IEndpoint
{
    public sealed record CreateExerciseRequest(string Name, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/exercises", async (CreateExerciseRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                // TODO: replace with authenticated user identity
                var result = await sender.Send(
                    new CreateExerciseCommand(request.Name, request.Description, "System"),
                    cancellationToken);

                return result.IsFailure
                    ? Results.BadRequest(result.Error)
                    : Results.CreatedAtRoute("GetExerciseById", new { id = result.Value }, result.Value);
            })
            .WithName("CreateExercise")
            .WithTags(Tags.Exercises)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}

