using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IUserRepository
    {
        User? GetByLogin(string login);

        User? GetById(Guid id);

        User? GetByEmail(string email);

        void Add(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User? GetById(Guid id)
        {
            return _db.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefault(u => u.UserId == id);
        }

        public User? GetByLogin(string login)
        {
            return _db.Users.FirstOrDefault(u => u.Login == login);
        }
    }

}
