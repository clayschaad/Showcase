using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Showcase.Measurement.Domain.Weather;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Application.Weather;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Test.IntegrationTests
{
    public class WeatherMeasurementServiceTests
    {
        private Mock<IWeatherMeasurement> weatherMeasurementMock  = new Mock<IWeatherMeasurement>();
        private Mock<IWeatherMeasurementPersistance> weatherMeasurementPersistanceMock = new Mock<IWeatherMeasurementPersistance>();
        private Mock<IMeasurementSender> weatherMeasurementSenderMock = new Mock<IMeasurementSender>();

        private List<Temperature> temperatureQueue = new List<Temperature>();
        private List<Pressure> pressureQueue = new List<Pressure>();

        [Test]
        public async Task MeasureTemperatureTest()
        {
            ArrangeWeatherMeasurementMocks(new WeatherRecord(Timestamp: System.DateTime.UtcNow, Latitude: 47.57, Longitude: 9.104, Temperature: 21.2, Pressure: 1234));
            var testee = new WeatherMeasurementService(weatherMeasurementMock.Object, weatherMeasurementPersistanceMock.Object, weatherMeasurementSenderMock.Object);

            await testee.MeasureWeatherAsync(latitude: 47.57, longitude: 9.104, CancellationToken.None);

            //Assert.That(temperatureQueue.Count, Is.EqualTo(1));
            //Assert.That(temperatureQueue.First().Value, Is.EqualTo(21.2));
            //Assert.That(temperatureQueue.First().Coordinates.Latitude, Is.EqualTo(47.57));
            //Assert.That(temperatureQueue.First().Coordinates.Longitude, Is.EqualTo(9.104));
            //Assert.That(pressureQueue.Count, Is.EqualTo(1));
            //Assert.That(pressureQueue.First().Value, Is.EqualTo(1234));
            //Assert.That(pressureQueue.First().Coordinates.Latitude, Is.EqualTo(47.57));
            //Assert.That(pressureQueue.First().Coordinates.Longitude, Is.EqualTo(9.104));
        }

        private void ArrangeWeatherMeasurementMocks(WeatherRecord weatherRecord)
        {
            weatherMeasurementMock
                .Setup(t => t.GetWeatherMeasurementAsync(It.IsAny<double>(), It.IsAny<double>(), CancellationToken.None))
                .Returns(Task.FromResult(weatherRecord));

            weatherMeasurementSenderMock
                .Setup(t => t.SendMeasurement(It.IsAny<Temperature>(), CancellationToken.None))
                .Callback((Temperature t, CancellationToken c) => temperatureQueue.Add(t));

            weatherMeasurementSenderMock
                .Setup(t => t.SendMeasurement(It.IsAny<Pressure>(), CancellationToken.None))
                .Callback((Pressure p, CancellationToken c) => pressureQueue.Add(p));
        }
    }
}