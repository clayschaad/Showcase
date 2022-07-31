using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Showcase.Infrastructure.Measurement.OpenWeatherMap;
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

            var result = await testee.GetWeatherMeasurementAsync(latitude: 47.57, longitude: 9.104, CancellationToken.None);

            Assert.That(result.Temperature, Is.InRange(-30, 40));
            Assert.That(result.Pressure, Is.InRange(500, 1500));
        }
    }
}