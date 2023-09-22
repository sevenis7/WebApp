using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interface;

namespace DataLayer.Repositories
{
    public class ProjectRepository : IBaseRepository<Project>
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public Project? Get(int id)
        {
            return _db.Projects.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Project> GetAll()
        {
            return _db.Projects;
        }

        public void Update(Project project)
        {
            _db.Projects.Update(project);
            _db.SaveChanges();
        }

        public void Remove(Project project)
        {
            _db.Projects.Remove(project);
            _db.SaveChanges();
        }

        public Project Add(Project project)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();
            return project;
        }
    }
}
