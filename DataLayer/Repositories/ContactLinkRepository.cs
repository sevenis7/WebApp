using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interface;

namespace DataLayer.Repositories
{
    public class ContactLinkRepository : IBaseRepository<ContactLink>
    {
        private readonly AppDbContext _db;

        public ContactLinkRepository(AppDbContext db)
        {
            _db = db;
        }

        public ContactLink Add(ContactLink entity)
        {
            _db.ContactLinks.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public ContactLink? Get(int id)
        {
            return _db.ContactLinks.FirstOrDefault(cl => cl.Id == id);
        }

        public IQueryable<ContactLink>? GetAll()
        {
            return _db.ContactLinks;
        }

        public void Remove(ContactLink entity)
        {
            _db.ContactLinks.Remove(entity);
        }

        public void Update(ContactLink entity)
        {
            _db.ContactLinks.Update(entity);
            _db.SaveChanges();
        }
    }
}
