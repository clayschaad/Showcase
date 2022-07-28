using Microsoft.AspNetCore.Mvc;
using Showcase.Domain;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PressureController : ControllerBase
    {
        private readonly IWeatherMeasurementService weatherMeasurementService;

        public PressureController(IWeatherMeasurementService weatherMeasurementService)
        {
            this.weatherMeasurementService = weatherMeasurementService;
        }

        [HttpGet(Name = "GetPressures")]
        public async Task<IReadOnlyList<Pressure>> GetAsync()
        {
            return await weatherMeasurementService.GetPressuresAsync(CancellationToken.None);
        }
    }
}
