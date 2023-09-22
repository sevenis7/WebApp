using DataLayer.Entities;
using DataLayer.Repositories;

namespace ServiceLayer.Interfaces
{
    public interface IRequestService
    {
        Request? Post(Request request, Guid userId);

        Request? ChangeStatus(int id, RequestStatus status);

        IQueryable<Request> GetByStatus(RequestStatus status);

        IQueryable<Request> All();

        IQueryable<Request> GetByTime(DateTime from, DateTime to);
    }
}
