using NUnit.Framework;
using Showcase.Domain.Measurements;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Showcase.Test
{
    public class TemperaturePersistanceTests
    {
        [Test]
        public async Task MemoryTemperaturePersistanceTest()
        {
            var temperaturePersistance = new Infrastructure.Persistence.Memory.TemperaturePersistance();
            await TestPersistance(temperaturePersistance, CancellationToken.None);
        }

        [Test]
        public async Task FileStorageTemperaturePersistanceTest()
        {
            var temperaturePersistance = new Infrastructure.Persistence.FileStorage.TemperaturePersistance();
            await TestPersistance(temperaturePersistance, CancellationToken.None);
        }

        private async Task TestPersistance(ITemperaturePersistance temperaturePersistance, CancellationToken cancellationToken)
        {
            var temperature = Temperature.NewMeasurement(12.4, DateTime.UtcNow);
            await temperaturePersistance.SaveTemperatureAsync(temperature, cancellationToken);
            var loadedTemperature = await temperaturePersistance.GetTemperatureAsync(temperature.Id, cancellationToken);

            Assert.That(loadedTemperature!.Value, Is.EqualTo(temperature.Value));
            Assert.That(loadedTemperature.Timestamp, Is.EqualTo(temperature.Timestamp));
            Assert.That(loadedTemperature.Id, Is.EqualTo(temperature.Id));
        }
    }
}