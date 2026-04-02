using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusclePlus4000.Application.WeatherForecasts.Dtos;
using MusclePlus4000.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace MusclePlus4000.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ISender sender) : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IReadOnlyList<WeatherForecastDto>> Get(CancellationToken cancellationToken)
        => await sender.Send(new GetWeatherForecastsQuery(), cancellationToken);
}
