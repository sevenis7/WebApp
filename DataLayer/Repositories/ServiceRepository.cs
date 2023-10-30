using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly AppDbContext _db;

        public ServiceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Service entity)
        {
            _db.Services.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> All() => await _db.Services.ToListAsync();

        public async Task Delete(Service entity)
        {
            _db.Services.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public Task<Service?> Get(int id) => _db.Services.FirstOrDefaultAsync(e => e.Id == id);

        public async Task Update(Service entity)
        {
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
