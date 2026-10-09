using Microsoft.Extensions.Caching.Memory;

namespace DublinBikesWeb.Services
{
    public class TimedBackgroundCache : BackgroundService // This class is a background service that runs periodically to cache Dublin Bikes station data.
    {
        private readonly ILogger<TimedBackgroundCache> _logger; // Logger for logging information and errors.
        private readonly IServiceScopeFactory _serviceScopeFactory; // Factory for creating service scopes, allowing the use of scoped services within the background service.

        private readonly IMemoryCache _memoryCache; // Memory cache for storing the Dublin Bikes station data.


        public TimedBackgroundCache(ILogger<TimedBackgroundCache> logger, IServiceScopeFactory serviceScopeFactory, IMemoryCache memoryCache) // Constructor that initializes the logger, service scope factory, and memory cache.
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _memoryCache = memoryCache;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) // This method is called when the background service starts and runs until the service is stopped.
        {
            _logger.LogInformation("Timed Hosted Service Running");

            await DoWork(stoppingToken);

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(5)); // Create a periodic timer that ticks every 5 minutes.

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken)) // Wait for the next tick of the timer, and if the service is not stopping, call the DoWork method to cache the station data.
                {
                    await DoWork(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Time Hosted Service is stopping");
            }


        }

        private async Task DoWork(CancellationToken stoppingToken) // This method retrieves the Dublin Bikes station data and caches it in memory.
        {


            try
            {
                await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope(); // Create a new service scope to resolve scoped services.

                IDublinBikeService dublinBikeService = scope.ServiceProvider.GetRequiredService<IDublinBikeService>(); // Resolve the IDublinBikeService from the service provider to fetch the station data.

                var StationData = await dublinBikeService.GetDublinBikesDataAsync(); // Call the GetDublinBikesDataAsync method to retrieve the station data.

                _memoryCache.Set(CacheKeys.StationData, StationData, TimeSpan.FromMinutes(5)); // Cache the station data in memory for 5 minutes using the specified cache key.

                _logger.LogInformation("Timed Hosted Service is working. LET'S GOOOOOOO");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining the stations data"); // Log any exceptions that occur while retrieving the station data.

            }


        }


    }
}
