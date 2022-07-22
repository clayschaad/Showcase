using NUnit.Framework;
using Showcase.Domain.Measurements;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Showcase.Test
{
    public class TemperatureServiceTests
    {
        private Mock<ITemperatureMeasurement> temperatureMeasurementMock  = new Mock<ITemperatureMeasurement>();
        private Mock<ITemperaturePersistance> temperaturePersistanceMock = new Mock<ITemperaturePersistance>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task MeasureTemperatureTest()
        {
            ArrangeTemperaturetMocks(21.2);
            var temperatureService = new TemperatureService(temperatureMeasurementMock.Object, temperaturePersistanceMock.Object);

            var temperature = await temperatureService.MeasureTemperatureAsync(CancellationToken.None);

            Assert.That(temperature.Value, Is.EqualTo(21.2));
            Assert.That(temperature.Id, Is.Not.EqualTo(Guid.Empty));
        }

        private void ArrangeTemperaturetMocks(double temperature)
        {
            temperatureMeasurementMock
                .Setup(t => t.GetTemperatureAsync(CancellationToken.None))
                .Returns(Task.FromResult(temperature));

            temperaturePersistanceMock
                .Setup(t => t.SaveTemperatureAsync(It.IsAny<Temperature>(), CancellationToken.None))
                .Returns(Task.FromResult(new Temperature
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.UtcNow,
                    Value = temperature
                }));
        }
    }
}