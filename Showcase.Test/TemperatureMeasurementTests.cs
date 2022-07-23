using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Showcase.Domain.Measurements;
using Showcase.Infrastructure.Measurement;
using System.Threading;
using System.Threading.Tasks;

namespace Showcase.Test
{
    public class TemperatureMeasurementTests
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
            var testee = new TemperatureMeasurement(configuration!);
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);

            var result = await testee.GetTemperatureAsync(coordinates, CancellationToken.None);

            Assert.That(result, Is.InRange(-30, 40));
        }
    }
}