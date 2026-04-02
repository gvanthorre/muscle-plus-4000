namespace MusclePlus4000.Application.WeatherForecasts.Dtos;

public sealed record WeatherForecastDto(
    DateOnly Date,
    int TemperatureC,
    int TemperatureF,
    string? Summary
);

