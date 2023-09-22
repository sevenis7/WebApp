using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interface;

namespace DataLayer.Repositories
{
    public class ArticleRepository : IBaseRepository<Article>
    {
        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContext db)
        {
            _db = db;
        }

        public Article Add(Article entity)
        {
            _db.Articles.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public Article? Get(int id)
        {
            return _db.Articles.FirstOrDefault(a => a.Id == id);
        }

        public IQueryable<Article>? GetAll()
        {
            return _db.Articles;
        }

        public void Remove(Article entity)
        {
            _db.Articles.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Article entity)
        {
            _db.Articles.Update(entity);
            _db.SaveChanges();
        }
    }
}
