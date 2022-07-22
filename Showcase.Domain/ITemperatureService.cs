using Showcase.Domain.Measurements;

namespace Showcase.Domain
{
    public interface ITemperatureService
    {
        Task<Temperature> MeasureTemperatureAsync(CancellationToken cancellationToken);
    }
}
