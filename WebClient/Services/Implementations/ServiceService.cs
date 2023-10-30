using DataLayer.Entities;
using System.Net.Http.Json;
using WebClient.Services.Interfaces;
using HttpClientJsonExtensions = System.Net.Http.Json.HttpClientJsonExtensions;

namespace WebClient.Services.Implementations
{
    public class ServiceService : IService<Service>
    {
        private readonly HttpClient _httpClient;

        public ServiceService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<IEnumerable<Service>> All()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Service>>("service") ?? Enumerable.Empty<Service>();

        public async Task<Service?> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"service/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Service>();
        }

        public async Task<Service?> Edit(int id, Service entity)
        {
            var response = await HttpClientJsonExtensions.PutAsJsonAsync(_httpClient, $"service/{id}", entity);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Service>();
        }

        public async Task<Service?> Get(int id)
        {
            var response = await _httpClient.GetAsync($"service/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Service>();
        }

        public async Task<Service?> Post(Service entity)
        {
            var response = await HttpClientJsonExtensions.PostAsJsonAsync(_httpClient, "service", entity);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Service>();
        }
    }
}
