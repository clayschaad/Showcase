using Microsoft.AspNetCore.Mvc;
using Showcase.Domain;
using Showcase.Domain.Measurements;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureService temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            this.temperatureService = temperatureService;
        }

        [HttpGet(Name = "GetTemperature")]
        public async Task<double> Get()
        {
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);
            var temperature = await temperatureService.MeasureTemperatureAsync(coordinates, CancellationToken.None);
            return temperature;
        }
    }
}
