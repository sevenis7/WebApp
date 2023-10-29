using DataLayer.Entities;
using ServiceLayer.Requests;
using System.Net.Http.Json;
using WebClient.Services.Interfaces;

namespace WebClient.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private string _error = "";

        public string Error => _error;

        public RequestService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<IEnumerable<Request?>> All()
        {
            return await _httpClient.GetFromJsonAsync<List<Request?>>("request") ?? Enumerable.Empty<Request?>();
        }

        public async Task<Request?> Post(PostRequest request)
        {
            var response = await HttpClientJsonExtensions.PostAsJsonAsync(_httpClient, "request", request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Request>();
        }

        public async Task<Request?> Get(int id)
        {
            var response = await _httpClient.GetAsync($"request/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _error = "There is no request with this id";

                return null;
            }

            return await response.Content.ReadFromJsonAsync<Request>();
        }

        public async Task<Request?> Put(int id, Request request)
        {
            var response = await HttpClientJsonExtensions.PutAsJsonAsync(_httpClient, $"request/{id}", request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Request>();
        }
    }
}
