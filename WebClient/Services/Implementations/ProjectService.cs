using DataLayer.Entities;
using System.Net.Http.Json;
using HttpClientJsonExtensions = System.Net.Http.Json.HttpClientJsonExtensions;
using IProjectService = WebClient.Services.Interfaces.IProjectService;

namespace WebClient.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<Project?> Get(int id)
        {
            var response = await _httpClient.GetAsync($"project/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsAsync<Project?>();
        }

        public async Task<IEnumerable<Project>> All()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Project>>("project") ?? Enumerable.Empty<Project>();
        }

        public async Task<Project?> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"project/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsAsync<Project>();
        }

        public async Task<Project?> Edit(int id, Project project)
        {
            var response = await HttpClientJsonExtensions.PutAsJsonAsync(_httpClient, $"project/{id}", project);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Project>();
        }

        public async Task<Project?> Post(Project project)
        {
            var response = await HttpClientJsonExtensions.PostAsJsonAsync(_httpClient, "project", project);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<Project>();
        }
    }
}
