using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _db;

        public RequestRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Request request)
        {
            await _db.Requests.AddAsync(request);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Request>> GetAll()
        {
            return await _db.Requests.Include(r => r.User).ToListAsync();
        }

        public async Task<Request?>? GetById(int id)
        {
            return await _db.Requests.Include(r => r.User).FirstOrDefaultAsync(r => r.RequestId == id);
        }

        public async Task Update(Request request)
        {
            _db.Requests.Update(request);
            await _db.SaveChangesAsync();
        }
    }
}
