using MediatR;
using MusclePlus4000.Api.Abstractions;
using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

namespace MusclePlus4000.Api.Exercises;

public sealed class GetAllExercises : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/exercises", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var exercises = await sender.Send(new GetAllExercisesQuery(), cancellationToken);
                return Results.Ok(exercises);
            })
            .WithName("GetAllExercises")
            .WithTags(Tags.Exercises)
            .Produces<IReadOnlyList<ExerciseListItemDto>>();
    }
}

