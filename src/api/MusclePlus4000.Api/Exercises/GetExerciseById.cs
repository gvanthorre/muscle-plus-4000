using MediatR;
using MusclePlus4000.Api.Abstractions;
using MusclePlus4000.Application.Exercises.Queries.GetExerciseById;
using MusclePlus4000.Domain.Common;

namespace MusclePlus4000.Api.Exercises;

public sealed class GetExerciseById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/exercises/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetExerciseByIdQuery(id), cancellationToken);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.Ok(result.Value);
            })
            .WithName("GetExerciseById")
            .WithTags(Tags.Exercises)
            .Produces<ExerciseDto>()
            .Produces<Error>(StatusCodes.Status404NotFound);
    }
}

