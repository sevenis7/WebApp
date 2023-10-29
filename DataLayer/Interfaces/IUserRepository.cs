using DataLayer.Entities;

namespace DataLayer.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?>? GetByEmail(string email);
        Task<User?>? GetById(Guid id);
        Task<User?>? GetByLogin(string login);
    }
}