using DublinBikesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DublinBikesWeb.Services
{
    public class DublinBikeService : IDublinBikeService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY")
            ?? throw new InvalidOperationException("API_KEY environment variable is not set.");
        private readonly HttpClient _createdHttpClient;

        public DublinBikeService(HttpClient createdHttpClient)
        {
            _createdHttpClient = createdHttpClient ?? throw new ArgumentNullException(nameof(createdHttpClient));
        }

        public async Task<IEnumerable<DublinBikesData>> GetDublinBikesDataAsync()
        {
            var response = await _createdHttpClient.GetFromJsonAsync<List<DublinBikesData>>(
                $"https://api.jcdecaux.com/vls/v3/stations?contract=dublin&apiKey={_apiKey}");

            return response ?? Enumerable.Empty<DublinBikesData>();
        }
    }
}
