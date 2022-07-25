using NUnit.Framework;
using Showcase.Domain.Measurements;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Test.IntegrationTests
{
    public class TemperatureServiceTests
    {
        private Mock<ITemperatureMeasurement> temperatureMeasurementMock  = new Mock<ITemperatureMeasurement>();
        private Mock<ITemperaturePersistance> temperaturePersistanceMock = new Mock<ITemperaturePersistance>();

        [Test]
        public async Task MeasureTemperatureTest()
        {
            ArrangeTemperaturetMocks(21.2);
            var testee = new TemperatureService(temperatureMeasurementMock.Object, temperaturePersistanceMock.Object);
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);

            var result = await testee.MeasureTemperatureAsync(coordinates, CancellationToken.None);

            Assert.That(result, Is.EqualTo(21.2));
        }

        private void ArrangeTemperaturetMocks(double temperature)
        {
            temperatureMeasurementMock
                .Setup(t => t.GetTemperatureAsync(It.IsAny<Coordinates>(), CancellationToken.None))
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