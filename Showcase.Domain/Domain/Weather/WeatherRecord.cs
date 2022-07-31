namespace Showcase.Measurement.Domain.Weather
{
    public record WeatherRecord(DateTime Timestamp, double Latitude, double Longitude, double Temperature, int Pressure);
}
