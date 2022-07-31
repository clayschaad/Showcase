using Microsoft.AspNetCore.Mvc;
using Showcase.Measurement.Application;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinanceMeasurementController : ControllerBase
    {
        private readonly IFinanceMeasurementService financeMeasurementService;

        public FinanceMeasurementController(IFinanceMeasurementService financeMeasurementService)
        {
            this.financeMeasurementService = financeMeasurementService;
        }

        [HttpPost(Name = "GetStock")]
        public async Task PostAsync(CancellationToken cancellation, DateTime date, string symbol = "UBS")
        {
            if (date == DateTime.MinValue) 
            {
                date = DateTime.Now.AddDays(-1);
            }

            await financeMeasurementService.MeasureStockAsync(symbol, date, cancellation);
        }
    }
}
