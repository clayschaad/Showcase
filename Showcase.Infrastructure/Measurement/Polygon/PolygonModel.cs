using System.Text.Json.Serialization;

namespace Showcase.Infrastructure.Measurement.Polygon
{
    public record PolygonModel(
        [property: JsonPropertyName("afterHours")] double AfterHours,
        [property: JsonPropertyName("close")] double Close,
        [property: JsonPropertyName("from")] DateTime From,
        [property: JsonPropertyName("high")] double High,
        [property: JsonPropertyName("low")] double Low,
        [property: JsonPropertyName("open")] double Open,
        [property: JsonPropertyName("preMarket")] double PreMarket,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("volume")] int Volume
    );
}
