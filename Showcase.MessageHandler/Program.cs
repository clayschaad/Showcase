using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Showcase.Infrastructure;
using Showcase.Infrastructure.Messaging;
using System.Reflection;

namespace Showcase.MessageHandler
{
    // https://dfederm.com/building-a-console-app-with-.net-generic-host/
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureLogging(logging =>
                {
                    // Add any 3rd party loggers like NLog or Serilog
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddHostedService<MessageQueueConsumerService>()
                        .AddInfrastructureServices(hostContext.Configuration);

                    services.AddOptions<MessagingOptions>().Bind(hostContext.Configuration.GetSection(MessagingOptions.SectionKey));
                })
                .RunConsoleAsync();
        }
    }
}


