using NUnit.Framework;
using Showcase.Domain.Measurements;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System;
using Showcase.Domain.Measurements.Weather;
using System.Collections.Generic;
using System.Linq;

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
            ArrangeWeatherMeasurementMocks(new WeatherMeasurement(Temperature: 21.2, Pressure: 1234));
            var testee = new WeatherMeasurementService(weatherMeasurementMock.Object, weatherMeasurementPersistanceMock.Object, weatherMeasurementSenderMock.Object);
            var coordinates = new Coordinates(Latitude: 47.57, Longitude: 9.104);

            await testee.MeasureWeatherAsync(coordinates, CancellationToken.None);

            Assert.That(temperatureQueue.Count, Is.EqualTo(1));
            Assert.That(temperatureQueue.First().Value, Is.EqualTo(21.2));
            Assert.That(temperatureQueue.First().Coordinates, Is.EqualTo(coordinates));
            Assert.That(pressureQueue.Count, Is.EqualTo(1));
            Assert.That(pressureQueue.First().Value, Is.EqualTo(1234));
            Assert.That(pressureQueue.First().Coordinates, Is.EqualTo(coordinates));
        }

        private void ArrangeWeatherMeasurementMocks(WeatherMeasurement weatherMeasurement)
        {
            weatherMeasurementMock
                .Setup(t => t.GetWeatherMeasurementAsync(It.IsAny<Coordinates>(), CancellationToken.None))
                .Returns(Task.FromResult(weatherMeasurement));

            weatherMeasurementSenderMock
                .Setup(t => t.SendMeasurement(It.IsAny<Temperature>(), CancellationToken.None))
                .Callback((Temperature t, CancellationToken c) => temperatureQueue.Add(t));

            weatherMeasurementSenderMock
                .Setup(t => t.SendMeasurement(It.IsAny<Pressure>(), CancellationToken.None))
                .Callback((Pressure p, CancellationToken c) => pressureQueue.Add(p));
        }
    }
}