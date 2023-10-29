using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task<Project?>? Get(int id)
        {
            return await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Delete(Project project)
        {
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>?>? GetAll()
        {
            return await _db.Projects.ToListAsync();
        }
    }
}
