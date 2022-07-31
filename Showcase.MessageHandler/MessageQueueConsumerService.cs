using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Showcase.Infrastructure.Messaging;
using Showcase.Infrastructure.Messaging.RabbitMQ;
using Showcase.Measurement.Application;
using Showcase.Measurement.Domain.Finance;
using Showcase.Measurement.Domain.Weather;

namespace Showcase.MessageHandler
{
    internal sealed class MessageQueueConsumerService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IOptions<MessagingOptions> messagingOptions;
        private readonly IWeatherMeasurementService weatherMeasurementService;
        private readonly IFinanceMeasurementService financeMeasurementService;

        public MessageQueueConsumerService(
            ILogger<MessageQueueConsumerService> logger, 
            IHostApplicationLifetime appLifetime, 
            IOptions<MessagingOptions> messagingOptions, 
            IWeatherMeasurementService weatherMeasurementService, 
            IFinanceMeasurementService financeMeasurementService)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.messagingOptions = messagingOptions;
            this.weatherMeasurementService = weatherMeasurementService;
            this.financeMeasurementService = financeMeasurementService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        logger.LogInformation("Starting Message Handler (please ensure rabbitmq is up)...");

                        var factory = new ConnectionFactory() { HostName = messagingOptions.Value.Hostname, Password = messagingOptions.Value.Password, UserName = messagingOptions.Value.Username, Port = messagingOptions.Value.Port };
                        using (var connection = factory.CreateConnection())
                        using (var channel = connection.CreateModel())
                        {
                            RabbitMqHelper.Init(channel, messagingOptions.Value.Queue);

                            RabbitMqHelper.Consume<WeatherRecord>(channel, messagingOptions.Value.Queue, async (T) =>
                            {
                                logger.LogInformation($"{T}");
                                await weatherMeasurementService.SaveWeatherAsync(T, CancellationToken.None);
                            });

                            RabbitMqHelper.Consume<StockRecord>(channel, messagingOptions.Value.Queue, async (T) =>
                            {
                                logger.LogInformation($"{T}");
                                await financeMeasurementService.SaveStockAsync(T, CancellationToken.None);
                            });

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                await Task.Delay(1000);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unhandled exception!");
                    }
                    finally
                    {
                        appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
