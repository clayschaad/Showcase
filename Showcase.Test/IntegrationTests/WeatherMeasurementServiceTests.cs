using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Showcase.Measurement.Domain.Weather;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Application.Weather;

namespace Showcase.Test.IntegrationTests
{
    public class WeatherMeasurementServiceTests
    {
        private Mock<IWeatherMeasurement> weatherMeasurementMock  = new Mock<IWeatherMeasurement>();
        private Mock<IWeatherMeasurementPersistance> weatherMeasurementPersistanceMock = new Mock<IWeatherMeasurementPersistance>();
        private Mock<IMeasurementSender> weatherMeasurementSenderMock = new Mock<IMeasurementSender>();

        private List<WeatherRecord> messageQueue = new List<WeatherRecord>();

        [Test]
        public async Task MeasureTemperatureTest()
        {
            ArrangeWeatherMeasurementMocks(new WeatherRecord(Timestamp: System.DateTime.UtcNow, Latitude: 47.57, Longitude: 9.104, Temperature: 21.2, Pressure: 1234));
            var testee = new WeatherMeasurementService(weatherMeasurementMock.Object, weatherMeasurementPersistanceMock.Object, weatherMeasurementSenderMock.Object);

            await testee.MeasureWeatherAsync(latitude: 47.57, longitude: 9.104, CancellationToken.None);

            Assert.That(messageQueue.Count, Is.EqualTo(1));
            Assert.That(messageQueue.First().Temperature, Is.EqualTo(21.2));
            Assert.That(messageQueue.First().Pressure, Is.EqualTo(1234));
            Assert.That(messageQueue.First().Latitude, Is.EqualTo(47.57));
            Assert.That(messageQueue.First().Longitude, Is.EqualTo(9.104));
        }

        private void ArrangeWeatherMeasurementMocks(WeatherRecord weatherRecord)
        {
            weatherMeasurementMock
                .Setup(t => t.GetWeatherMeasurementAsync(It.IsAny<double>(), It.IsAny<double>(), CancellationToken.None))
                .Returns(Task.FromResult(weatherRecord));

            weatherMeasurementSenderMock
                .Setup(t => t.SendMeasurement(It.IsAny<WeatherRecord>(), CancellationToken.None))
                .Callback((WeatherRecord w, CancellationToken c) => messageQueue.Add(w));
        }
    }
}