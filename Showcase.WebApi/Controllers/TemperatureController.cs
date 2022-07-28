﻿using Microsoft.AspNetCore.Mvc;
using Showcase.Domain;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly IWeatherMeasurementService weatherMeasurementService;

        public TemperatureController(IWeatherMeasurementService weatherMeasurementService)
        {
            this.weatherMeasurementService = weatherMeasurementService;
        }

        [HttpGet(Name = "GetTemperatures")]
        public async Task<IReadOnlyList<Temperature>> GetAsync()
        {
            return await weatherMeasurementService.GetTemperaturesAsync(CancellationToken.None);
        }
    }
}
