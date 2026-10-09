using DublinBikesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DublinBikesWeb.Services
{
    public class DublinBikeService : IDublinBikeService // Implementation of the DublinBikeService class that retrieves bike station data from the JCDecaux API.
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("API_KEY")
            ?? throw new InvalidOperationException("API_KEY environment variable is not set.");
        private readonly HttpClient _createdHttpClient;

        public DublinBikeService(HttpClient createdHttpClient) // Constructor that initializes the HttpClient for making API requests.
        {
            _createdHttpClient = createdHttpClient ?? throw new ArgumentNullException(nameof(createdHttpClient)); // Ensure that the HttpClient is not null and initialize it
        }

        public async Task<IEnumerable<DublinBikesData>> GetDublinBikesDataAsync() // Method that retrieves bike station data from the JCDecaux API asynchronously.
        {
            var response = await _createdHttpClient.GetFromJsonAsync<List<DublinBikesData>>(
                $"https://api.jcdecaux.com/vls/v3/stations?contract=dublin&apiKey={_apiKey}"); // Make an HTTP GET request to the JCDecaux API and deserialize the JSON response into a list of DublinBikesData objects.

            return response ?? Enumerable.Empty<DublinBikesData>(); // Return the response or an empty list if the response is null.
        }
    }

}
