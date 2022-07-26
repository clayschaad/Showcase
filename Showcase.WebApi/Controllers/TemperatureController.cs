using Microsoft.AspNetCore.Mvc;
using Showcase.Domain;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;

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

        [HttpPost(Name = "SetTemperature")]
        public async Task<double> PostAsync()
        {
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);
            var temperature = await temperatureService.MeasureTemperatureAsync(coordinates, CancellationToken.None);
            return temperature;
        }

        [HttpGet(Name = "GetTemperatures")]
        public async Task<IReadOnlyList<Temperature>> GetAsync()
        {
            var temperatures = await temperatureService.GetTemperatures(CancellationToken.None);
            return temperatures;
        }
    }
}
