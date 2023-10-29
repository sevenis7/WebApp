using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticle>> All();
        Task<BlogArticle?> Delete(int id);
        Task<BlogArticle?> Edit(int id, BlogArticle article);
        Task<BlogArticle?> Get(int id);
        Task<BlogArticle?> Post(BlogArticle article);
    }
}