using Microsoft.AspNetCore.Mvc;
using Showcase.Domain;
using Showcase.Domain.Measurements;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherMeasurementService weatherMeasurementService;

        public WeatherController(IWeatherMeasurementService weatherMeasurementService)
        {
            this.weatherMeasurementService = weatherMeasurementService;
        }

        [HttpPost(Name = "MeasureWeather")]
        public async Task PostAsync()
        {
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);
            await weatherMeasurementService.MeasureWeatherAsync(coordinates, CancellationToken.None);
        }
    }
}
