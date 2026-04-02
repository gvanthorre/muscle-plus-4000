using MediatR;
using MusclePlus4000.Application.WeatherForecasts.Dtos;

namespace MusclePlus4000.Application.WeatherForecasts.Queries.GetWeatherForecasts;

/// <summary>Query that returns a list of weather forecast DTOs.</summary>
public sealed record GetWeatherForecastsQuery : IRequest<IReadOnlyList<WeatherForecastDto>>;

