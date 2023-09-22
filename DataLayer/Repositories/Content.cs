using DataLayer.Context;
using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public interface IContent
    {
        MainContent Get();

        void Update();
    }

    public class Content : IContent
    {
        private readonly AppDbContext _db;

        public Content(AppDbContext db)
        {
            _db = db;
        }

        public MainContent Get()
        {
            return _db.MainContent.Single();
        }

        public void Update()
        {
            _db.MainContent.Update(_db.MainContent.Single());
            _db.SaveChanges();
        }

    }
}
