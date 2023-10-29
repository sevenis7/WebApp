using DataLayer.Entities;

namespace DataLayer.Interfaces
{
    public interface IBlogArticleRepository
    {
        Task Add(BlogArticle article);
        Task Delete(BlogArticle article);
        Task<BlogArticle?> Get(int id);
        Task<IEnumerable<BlogArticle>> GetAll();
        Task Update(BlogArticle article);
    }
}