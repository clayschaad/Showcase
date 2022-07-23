using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Test
{
    public class TemperaturePersistanceTests
    {
        [Test]
        public async Task MemoryTemperaturePersistanceTest()
        {
            var testee = new Infrastructure.Persistence.Memory.TemperaturePersistance();
            await TestPersistance(testee, CancellationToken.None);
        }

        [Test]
        public async Task FileStorageTemperaturePersistanceTest()
        {
            var testee = new Infrastructure.Persistence.FileStorage.TemperaturePersistance();
            await TestPersistance(testee, CancellationToken.None);
        }

        private async Task TestPersistance(ITemperaturePersistance testee, CancellationToken cancellationToken)
        {
            var temperature = Temperature.NewMeasurement(12.4, DateTime.UtcNow);
            await testee.SaveTemperatureAsync(temperature, cancellationToken);
            var result = await testee.GetTemperatureAsync(temperature.Id, cancellationToken);

            Assert.That(result!.Value, Is.EqualTo(temperature.Value));
            Assert.That(result.Timestamp, Is.EqualTo(temperature.Timestamp));
            Assert.That(result.Id, Is.EqualTo(temperature.Id));
        }
    }
}