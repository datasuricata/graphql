using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using vitrine_backend.Trace;

namespace vitrine_backend.Background.Publishers
{
    public interface ILoggerPublisher
    {
        Task Publish(Log log, CancellationToken cancellationToken = default);
    }

    public class LoggerPublisher : ILoggerPublisher
    {
        private readonly ILogger<LoggerPublisher> logger;

        public LoggerPublisher(ILogger<LoggerPublisher> logger)
        {
            this.logger = logger;
        }

        public async Task Publish(Log log, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("doing massive publishing logic to event hub...");

            await Task.Delay(200, cancellationToken);

            logger.LogInformation($"event published: {log.Id}");

            logger.LogInformation(log.Payload);
        }
    }
}
