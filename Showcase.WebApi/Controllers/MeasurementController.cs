using Microsoft.AspNetCore.Mvc;
using Showcase.Measurement.Application;

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
            await weatherMeasurementService.MeasureWeatherAsync(latitude: 47.57, longitude: 9.104, cancellation);

            var date = DateTime.Now.AddDays(-1);
            await financeMeasurementService.MeasureStockAsync("UBS", date, cancellation);
        }
    }
}
