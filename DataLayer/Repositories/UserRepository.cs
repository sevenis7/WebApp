using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User?>? GetByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?>? GetById(Guid id)
        {
            return await _db.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?>? GetByLogin(string login)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

    }

}
