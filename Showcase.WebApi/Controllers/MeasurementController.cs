using Microsoft.AspNetCore.Mvc;
using Showcase.Measurement.Application;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IWeatherMeasurementService weatherMeasurementService;
        private readonly IFinanceMeasurementService financeMeasurementService;

        public MeasurementController(IWeatherMeasurementService weatherMeasurementService, IFinanceMeasurementService financeMeasurementService)
        {
            this.weatherMeasurementService = weatherMeasurementService;
            this.financeMeasurementService = financeMeasurementService;
        }

        [HttpPost(Name = "Measure")]
        public async Task PostAsync(CancellationToken cancellation)
        {
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);
            await weatherMeasurementService.MeasureWeatherAsync(coordinates, cancellation);

            var date = DateTime.Now.AddDays(-1);
            await financeMeasurementService.MeasureStockAsync("UBS", date, cancellation);
        }
    }
}
