using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Domain
{
    public interface ITemperatureService
    {
        Task<double> MeasureTemperatureAsync(Coordinates coordinates, CancellationToken cancellationToken);
        Task<IReadOnlyList<Temperature>> GetTemperatures(CancellationToken cancellationToken);
    }
}
