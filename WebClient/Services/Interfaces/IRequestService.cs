using DataLayer.Entities;
using ServiceLayer.DTO;
using ServiceLayer.Requests;

namespace WebClient.Services.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<Request?>> All();
        Task<Request?> Get(int id);
        Task<Request?> Post(PostRequest request);
        Task<Request?> Put(int id, Request request);
        string Error { get; }
    }
}