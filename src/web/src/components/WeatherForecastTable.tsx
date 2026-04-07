import { useQuery } from "@tanstack/react-query";
import type { WeatherForecast } from "../types/weather";
import { apiClient } from "../config/axios.ts";

const fetchWeather = async (): Promise<WeatherForecast[]> => {
    const { data } = await apiClient.get<WeatherForecast[]>("/api/weatherforecast");
    return data;
};

const summaryEmoji: Record<string, string> = {
    Freezing: "🥶",
    Bracing: "🧊",
    Chilly: "🌬️",
    Cool: "😎",
    Mild: "🌤️",
    Warm: "☀️",
    Balmy: "🌈",
    Hot: "🔥",
    Sweltering: "😰",
    Scorching: "🌡️",
};

export function WeatherForecastTable() {
    const { data, isLoading, isError, error } = useQuery({
        queryKey: ["weatherForecast"],
        queryFn: fetchWeather,
    });

    if (isLoading) {
        return (
            <div className="weather-status">
                <span className="spinner" />
                Loading forecast...
            </div>
        );
    }

    if (isError) {
        return (
            <div className="weather-status weather-error">
                Failed to load forecast: {(error as Error).message}
            </div>
        );
    }

    return (
        <table className="weather-table">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Summary</th>
                    <th>Temp (C)</th>
                    <th>Temp (F)</th>
                </tr>
            </thead>
            <tbody>
                {data?.map((item) => (
                    <tr key={item.date}>
                        <td>
                            {new Date(item.date).toLocaleDateString(undefined, {
                                weekday: "short",
                                month: "short",
                                day: "numeric",
                            })}
                        </td>
                        <td>
                            {summaryEmoji[item.summary ?? ""] ?? "🌫️"} {item.summary}
                        </td>
                        <td>{item.temperatureC} °C</td>
                        <td>{item.temperatureF} °F</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
