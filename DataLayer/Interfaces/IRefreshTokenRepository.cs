using DataLayer.Entities;

namespace DataLayer.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task Add(RefreshToken refreshToken);
        Task Delete(RefreshToken refreshToken);
        Task<RefreshToken?>? GetById(Guid id);
        Task<RefreshToken?>? GetByToken(string token);
    }
}