using System.Text.Json.Serialization;

namespace Showcase.Infrastructure.Measurement.OpenWeatherMap
{
    public record WeatherMeasurement(
        [property: JsonPropertyName("coord")] Coord Coord,
        [property: JsonPropertyName("weather")] IReadOnlyList<Weather> Weather,
        [property: JsonPropertyName("base")] string Base,
        [property: JsonPropertyName("main")] Main Main,
        [property: JsonPropertyName("visibility")] int Visibility,
        [property: JsonPropertyName("wind")] Wind Wind,
        [property: JsonPropertyName("clouds")] Clouds Clouds,
        [property: JsonPropertyName("dt")] int Dt,
        [property: JsonPropertyName("sys")] Sys Sys,
        [property: JsonPropertyName("timezone")] int Timezone,
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("cod")] int Cod
    );

    public record Clouds(
        [property: JsonPropertyName("all")] int All
    );

    public record Coord(
        [property: JsonPropertyName("lon")] double Lon,
        [property: JsonPropertyName("lat")] double Lat
    );

    public record Main(
        [property: JsonPropertyName("temp")] double Temp,
        [property: JsonPropertyName("feels_like")] double FeelsLike,
        [property: JsonPropertyName("temp_min")] double TempMin,
        [property: JsonPropertyName("temp_max")] double TempMax,
        [property: JsonPropertyName("pressure")] int Pressure,
        [property: JsonPropertyName("humidity")] int Humidity
    );

    public record Sys(
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("country")] string Country,
        [property: JsonPropertyName("sunrise")] int Sunrise,
        [property: JsonPropertyName("sunset")] int Sunset
    );

    public record Weather(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("main")] string Main,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("icon")] string Icon
    );

    public record Wind(
        [property: JsonPropertyName("speed")] double Speed,
        [property: JsonPropertyName("deg")] int Deg,
        [property: JsonPropertyName("gust")] double Gust
    );


}
