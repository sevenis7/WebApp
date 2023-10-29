using DataLayer.Entities;

namespace WebClient.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> All();
        Task<Project?> Delete(int id);
        Task<Project?> Edit(int id, Project project);
        Task<Project?> Get(int id);
        Task<Project?> Post(Project project);
    }
}