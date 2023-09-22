using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interface;

namespace DataLayer.Repositories
{
    public class ServiceRepository : IBaseRepository<Service>
    {
        private readonly AppDbContext _db;

        public ServiceRepository(AppDbContext db)
        {
            _db = db;
        }

        public Service? Get(int id)
        {
            return _db.Services.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Service> GetAll()
        {
            return _db.Services;
        }

        public void Update(Service service)
        {
            _db.Services.Update(service);
            _db.SaveChanges();
        }

        public void Remove(Service service)
        {
            _db.Services.Remove(service);
            _db.SaveChanges();
        }

        public Service Add(Service service)
        {
            _db.Services.Add(service);
            _db.SaveChanges();
            return service;
        }
    }
}
