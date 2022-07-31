namespace Showcase.Measurement.Domain.Weather
{
    public record WeatherRecord(DateTime Timestamp, double Latitude, double Longitude, string City, string Country, double Temperature, int Pressure);
}
