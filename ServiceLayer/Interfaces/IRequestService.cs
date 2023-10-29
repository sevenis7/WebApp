using DataLayer.Entities;
using ServiceLayer.DTO;
using ServiceLayer.RequestServices;

namespace ServiceLayer.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<Request?>> All();
        Task<Request?> Edit(int id, Request request);
        Task<Request?> Post(Request request, Guid userId);
        Task<Request?> Get(int id);
    }
}