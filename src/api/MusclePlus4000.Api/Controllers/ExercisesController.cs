using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusclePlus4000.Application.Exercises.Queries.GetAllExercises;

namespace MusclePlus4000.Api.Controllers;

[ApiController]
[Route("api/exercises")]
public sealed class ExercisesController(ISender sender) : ControllerBase
{
    [HttpGet(Name = "GetAllExercises")]
    [ProducesResponseType(typeof(IReadOnlyList<ExerciseListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ExerciseListItemDto>>> GetAll(CancellationToken cancellationToken)
    {
        var exercises = await sender.Send(new GetAllExercisesQuery(), cancellationToken);
        return Ok(exercises);
    }
}

