using DataLayer.Entities;
using DataLayer.Interface;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ArticleService : IBaseService<Article>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public ArticleService(IBaseRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Article? Create(Article entity)
        {
            _articleRepository.Add(entity);

            return entity;
        }

        public bool Delete(int id)
        {
            var existringArticleById = _articleRepository.Get(id);

            if (existringArticleById != null)
            {
                _articleRepository.Remove(existringArticleById);
                return true;
            }
            return false;
        }

        public bool Edit(int id, Article entity)
        {
            var existringArticleById = _articleRepository.Get(id);

            if (existringArticleById == null || entity == null) return false;

            existringArticleById.Title = entity.Title;
            existringArticleById.Description = entity.Description;

            _articleRepository.Update(existringArticleById);

            return true;
        }

        public IQueryable<Article>? GetAll()
        {
            return _articleRepository.GetAll();
        }
    }
}
