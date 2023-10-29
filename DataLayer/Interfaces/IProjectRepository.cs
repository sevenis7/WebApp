using DataLayer.Entities;

namespace DataLayer.Interfaces
{
    public interface IProjectRepository
    {
        Task Delete(Project project);
        Task<Project?>? Get(int id);
        Task Update(Project project);
        Task Add(Project project);
        Task<IEnumerable<Project>?>? GetAll();
    }
}