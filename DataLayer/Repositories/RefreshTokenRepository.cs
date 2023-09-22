using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken);

        void Delete(RefreshToken? refreshToken);

        RefreshToken? GetById(Guid? id);

        RefreshToken? GetByToken(string token);
    }

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _db;

        public RefreshTokenRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            _db.SaveChanges();
        }

        public void Delete(RefreshToken? refreshToken)
        {
            _db.RefreshTokens.Remove(refreshToken);
            _db.SaveChanges();
        }

        public RefreshToken? GetById(Guid? id)
        {
            return _db.RefreshTokens.FirstOrDefault(t => t.Id == id);
        }

        public RefreshToken? GetByToken(string token)
        {
            return _db.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefault(r => r.Token == token);
        }
    }
}
