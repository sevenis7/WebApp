using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _db;

        public RefreshTokenRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(RefreshToken refreshToken)
        {
            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Remove(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?>? GetById(Guid id)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<RefreshToken?>? GetByToken(string token)
        {
            return await _db.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
