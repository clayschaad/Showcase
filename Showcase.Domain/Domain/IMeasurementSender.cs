namespace Showcase.Measurement.Domain
{
    public interface IMeasurementSender
    {
        Task SendMeasurement<T>(T measurement, CancellationToken cancellationToken) where T : notnull;
    }
}
