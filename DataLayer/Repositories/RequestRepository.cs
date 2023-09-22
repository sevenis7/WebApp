using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IRequestRepository
    {
        void Add(Request request);

        void Update(Request request);

        Request? GetById(int id);

        IQueryable<Request> GetWithStatus(RequestStatus status);

        IQueryable<Request> GetAll();

        IQueryable<Request> GetByTime(DateTime from, DateTime to);
    }

    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _db;

        public RequestRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Add(Request request)
        {
            _db.Requests.Add(request);
            _db.SaveChanges();
        }

        public IQueryable<Request> GetAll()
        {
            return _db.Requests.Include(r => r.User);
        }

        public Request? GetById(int id)
        {
            return _db.Requests.FirstOrDefault(r => r.RequestId == id);
        }

        public IQueryable<Request> GetByTime(DateTime from, DateTime to)
        {
            return _db.Requests.Where(r => r.DateTime >= from && r.DateTime <= to);
        }

        public IQueryable<Request> GetWithStatus(RequestStatus status)
        {
            return _db.Requests.Where(r => r.Status == status);
        }

        public void Update(Request request)
        {
            _db.Requests.Update(request);
            _db.SaveChanges();
        }
    }
}
