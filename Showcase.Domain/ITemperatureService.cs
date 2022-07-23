using Showcase.Domain.Measurements;

namespace Showcase.Domain
{
    public interface ITemperatureService
    {
        Task<double> MeasureTemperatureAsync(Coordinates coordinates, CancellationToken cancellationToken);
    }
}
