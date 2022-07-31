using Microsoft.AspNetCore.Mvc;
using Showcase.Measurement.Application;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherMeasurementController : ControllerBase
    {
        private readonly IWeatherMeasurementService weatherMeasurementService;

        public WeatherMeasurementController(IWeatherMeasurementService weatherMeasurementService)
        {
            this.weatherMeasurementService = weatherMeasurementService;
        }

        [HttpPost(Name = "GetWeather")]
        public async Task PostAsync(CancellationToken cancellationToken, double latitude = 47.56565386661019, double longitude = 9.108359461592157)
        {
            await weatherMeasurementService.MeasureWeatherAsync(latitude: latitude, longitude: longitude, cancellationToken);
        }
    }
}
