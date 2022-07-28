using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.Test.UnitTests
{
    public class WeatherMeasurementPersistanceTests
    {
        [Test]
        public async Task MemoryTemperaturePersistanceTest()
        {
            var testee = new Infrastructure.Persistence.Memory.MemoryTemperaturePersistance();
            var coordinates = new Coordinates(Latitude: 22.6, Longitude: 9);
            var temperature = Temperature.NewMeasurement(12.4, DateTime.UtcNow, coordinates);

            await testee.SaveTemperatureAsync(temperature, CancellationToken.None);
            var result = await testee.GetTemperatureAsync(temperature.Id, CancellationToken.None);

            Assert.That(result!.Value, Is.EqualTo(temperature.Value));
            Assert.That(result.Timestamp, Is.EqualTo(temperature.Timestamp));
            Assert.That(result.Id, Is.EqualTo(temperature.Id));
            Assert.That(result.Coordinates, Is.EqualTo(coordinates));
        }
    }
}