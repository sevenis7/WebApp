using DataLayer.Entities;

namespace DataLayer.Interfaces
{
    public interface IRequestRepository
    {
        Task Add(Request request);
        Task<IEnumerable<Request>> GetAll();
        Task<Request?>? GetById(int id);
        Task Update(Request request);
    }
}