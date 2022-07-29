namespace Showcase.Domain.Measurements
{
    public interface IMeasurementSender
    {
        Task SendMeasurement<T>(T measurement, CancellationToken cancellationToken) where T : notnull;
    }
}
