using DataLayer.Entities;
using System.Net.Http.Json;
using WebClient.Services.Interfaces;
using HttpClientJsonExtensions = System.Net.Http.Json.HttpClientJsonExtensions;

namespace WebClient.Services.Implementations
{
    public class BlogArticleService : IService<BlogArticle>
    {
        private readonly HttpClient _httpClient;

        public BlogArticleService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<BlogArticle?> Get(int id)
        {
            var response = await _httpClient.GetAsync($"blogarticle/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<BlogArticle>();
        }

        public async Task<IEnumerable<BlogArticle>> All()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BlogArticle>>("blogarticle") ?? Enumerable.Empty<BlogArticle>();
        }

        public async Task<BlogArticle?> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"blogarticle/{id}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<BlogArticle>();
        }

        public async Task<BlogArticle?> Post(BlogArticle blogArticle)
        {
            var response = await HttpClientJsonExtensions.PostAsJsonAsync(_httpClient, "blogarticle", blogArticle);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<BlogArticle>();
        }

        public async Task<BlogArticle?> Edit(int id, BlogArticle entity)
        {
            var response = await HttpClientJsonExtensions.PostAsJsonAsync(_httpClient, $"blogarticle/{id}", entity);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<BlogArticle>();
        }
    }
}
