using DataLayer.Entities;

namespace WebClient.Services.Interfaces
{
    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticle>> All();
        Task<BlogArticle?> Delete(int id);
        Task<BlogArticle?> Get(int id);
        Task<BlogArticle?> Post(BlogArticle blogArticle);
    }
}