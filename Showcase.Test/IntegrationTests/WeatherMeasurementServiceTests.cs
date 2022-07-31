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
            var weatherRecord = new WeatherRecord(
                Timestamp: System.DateTime.UtcNow,
                Latitude: 47.36,
                Longitude: 8.53,
                City: "Zurich",
                Country: "CH",
                Temperature: 21.2,
                Pressure: 1234);
            ArrangeWeatherMeasurementMocks(weatherRecord);
            var testee = new WeatherMeasurementService(weatherMeasurementMock.Object, weatherMeasurementSenderMock.Object, weatherMeasurementPersistanceMock.Object);

            await testee.MeasureWeatherAsync(latitude: weatherRecord.Latitude, longitude: weatherRecord.Longitude, CancellationToken.None);

            Assert.That(messageQueue.Count, Is.EqualTo(1));
            Assert.That(messageQueue.First().Temperature, Is.EqualTo(weatherRecord.Temperature));
            Assert.That(messageQueue.First().Pressure, Is.EqualTo(weatherRecord.Pressure));
            Assert.That(messageQueue.First().City, Is.EqualTo(weatherRecord.City));
            Assert.That(messageQueue.First().Country, Is.EqualTo(weatherRecord.Country));
            Assert.That(messageQueue.First().Latitude, Is.EqualTo(weatherRecord.Latitude));
            Assert.That(messageQueue.First().Longitude, Is.EqualTo(weatherRecord.Longitude));
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