using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Showcase.Infrastructure.Measurement.OpenWeatherMap;
using Showcase.Measurement.Domain.Weather;
using System.Threading;
using System.Threading.Tasks;

namespace Showcase.Test.UnitTests
{
    public class WeatherMeasurementTests
    {
        private IConfiguration? configuration;

        [SetUp]
        public void Setup()
        {
            configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json")
                .Build();
        }

        [Test]
        public async Task TemperatureMeasurementTest()
        {
            var testee = new OpenWeatherMapMeasurement(configuration!);
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);

            var result = await testee.GetWeatherMeasurementAsync(coordinates, CancellationToken.None);

            Assert.That(result.Temperature, Is.InRange(-30, 40));
            Assert.That(result.Pressure, Is.InRange(500, 1500));
        }
    }
}