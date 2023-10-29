using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project?> Get(int id)
        {
            return await _projectRepository.Get(id);
        }

        public async Task<Project?> Post(Project project)
        {
            if (project == null) return null;

            await _projectRepository.Add(project);

            return project;
        }

        public async Task<IEnumerable<Project>?> All() => await _projectRepository.GetAll();

        public async Task<Project?> Edit(int id, Project project)
        {
            var projectExistingById = await _projectRepository.Get(id);

            if (projectExistingById == null || project == null) return null;

            //projectExistingById.Title = project.Title;
            //projectExistingById.Description = project.Description;
            //projectExistingById.ImageBase64 = project.ImageBase64;

            projectExistingById = project;

            await _projectRepository.Update(projectExistingById);

            return projectExistingById;
        }

        public async Task<Project?> Delete(int id)
        {
            var projectExistingById = await Get(id);

            if (projectExistingById == null) return null;

            await _projectRepository.Delete(projectExistingById);

            return projectExistingById;
        }
    }
}
