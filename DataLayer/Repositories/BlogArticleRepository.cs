using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private readonly AppDbContext _db;

        public BlogArticleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(BlogArticle article)
        {
            await _db.BlogArticles.AddAsync(article);
            await _db.SaveChangesAsync();
        }

        public async Task<BlogArticle?> Get(int id)
        {
            return await _db.BlogArticles.FirstOrDefaultAsync(ba => ba.Id == id);
        }

        public async Task Delete(BlogArticle article)
        {
            _db.BlogArticles.Remove(article);
            await _db.SaveChangesAsync();
        }

        public async Task Update(BlogArticle article)
        {
            _db.BlogArticles.Update(article);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogArticle>> GetAll()
        {
            return await _db.BlogArticles.ToListAsync();
        }
    }
}
