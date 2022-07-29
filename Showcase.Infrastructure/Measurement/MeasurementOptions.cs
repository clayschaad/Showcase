namespace Showcase.Infrastructure.Measurement
{
    public class MeasurementOptions
    {
        public const string SectionKey = "Measurement";

        public string OpenWeatherMapApiKey { get; set; } = String.Empty;
        public string PolygonApiKey { get; set; } = String.Empty;
    }
}
