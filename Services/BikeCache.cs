using Microsoft.Extensions.Caching.Memory;

namespace DublinBikesWeb.Services
{
    public class TimedBackgroundCache : BackgroundService
    {
        private readonly ILogger<TimedBackgroundCache> _logger;
        private int _executionCount;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMemoryCache _memoryCache;


        public TimedBackgroundCache(ILogger<TimedBackgroundCache> logger, IServiceScopeFactory serviceScopeFactory, IMemoryCache memoryCache)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _memoryCache = memoryCache;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service Running");

            //When the timer should have no due-time, then do the work once now.
            await DoWork(stoppingToken);

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(5));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await DoWork(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Time Hosted Service is stopping");
            }


        }

        private async Task DoWork(CancellationToken stoppingToken)
        {


            try
            {
                await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();

                IDublinBikeService dublinBikeService = scope.ServiceProvider.GetRequiredService<IDublinBikeService>();

                var StationData = await dublinBikeService.GetDublinBikesDataAsync();

                _memoryCache.Set(CacheKeys.StationData, StationData, TimeSpan.FromMinutes(5));

                _logger.LogInformation("Timed Hosted Service is working. LET'S GOOOOOOO");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining the stations data");

            }


        }


    }
}
