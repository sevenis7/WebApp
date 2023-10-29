using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly IBlogArticleRepository _blogArticleRepository;

        public BlogArticleService(IBlogArticleRepository blogArticleRepository)
        {
            _blogArticleRepository = blogArticleRepository;
        }

        public async Task<BlogArticle?> Get(int id)
        {
            return await _blogArticleRepository.Get(id);
        }

        public async Task<BlogArticle?> Post(BlogArticle article)
        {
            if (article == null) return null;

            article.PublicationDate = DateTime.Now;

            await _blogArticleRepository.Add(article);

            return article;
        }

        public async Task<BlogArticle?> Delete(int id)
        {
            var artilceExistingById = await Get(id);

            if (artilceExistingById == null) return null;

            await _blogArticleRepository.Delete(artilceExistingById);

            return artilceExistingById;
        }

        public async Task<BlogArticle?> Edit(int id, BlogArticle article)
        {
            var articleExistingById = await Get(id);

            if (articleExistingById == null || article == null) return null;

            articleExistingById.Title = article.Title;
            articleExistingById.Text = article.Text;
            articleExistingById.Description = article.Description;
            articleExistingById.ImageBase64 = article.ImageBase64;

            await _blogArticleRepository.Update(articleExistingById);

            return articleExistingById;
        }

        public async Task<IEnumerable<BlogArticle>> All() => await _blogArticleRepository.GetAll();
    }
}
