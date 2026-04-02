using MediatR;
using MusclePlus4000.Application.WeatherForecasts.Dtos;

namespace MusclePlus4000.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public sealed class GetWeatherForecastsQueryHandler
    : IRequestHandler<GetWeatherForecastsQuery, IReadOnlyList<WeatherForecastDto>>
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public Task<IReadOnlyList<WeatherForecastDto>> Handle(
        GetWeatherForecastsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<WeatherForecastDto> forecasts = Enumerable
            .Range(1, 5)
            .Select(index =>
            {
                var tempC = Random.Shared.Next(-20, 55);
                return new WeatherForecastDto(
                    Date: DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC: tempC,
                    TemperatureF: 32 + (int)(tempC / 0.5556),
                    Summary: Summaries[Random.Shared.Next(Summaries.Length)]
                );
            })
            .ToList();

        return Task.FromResult(forecasts);
    }
}

