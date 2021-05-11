using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using vitrine_backend.Background;
using vitrine_backend.Background.Publishers;
using vitrine_backend.Trace;

namespace vitrine_backend
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<Log> queue;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<BackgroundWorker> logger;

        public BackgroundWorker(IBackgroundQueue<Log> queue, IServiceScopeFactory scopeFactory, ILogger<BackgroundWorker> logger)
        {
            this.queue = queue;
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("{Type} is now running in the background", nameof(BackgroundWorker));

            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogCritical("the {Type} is stopping due to a host shutdown, queued items might not be processed anymore",nameof(BackgroundWorker));

            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var threshold = 100;

                    await Task.Delay(threshold, stoppingToken);

                    var log = queue.Dequeue();

                    if (log == null) continue;

                    logger.LogInformation("item found on queue! starting to process...");

                    using var scope = scopeFactory.CreateScope();

                    var publisher = scope.ServiceProvider.GetRequiredService<ILoggerPublisher>();

                    await publisher.Publish(log, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogCritical("an error occurred when publishing a log. exception: {@Exception}", ex);
                }
            }
        }
    }
}
