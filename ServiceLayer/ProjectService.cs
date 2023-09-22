using DataLayer.Entities;
using DataLayer.Interface;
using DataLayer.Repositories;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ProjectService : IBaseService<Project>
    {
        private readonly IBaseRepository<Project> _projectRepository;

        public ProjectService(IBaseRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Project? Create(Project project)
        {
            _projectRepository.Add(project);

            return project;
        }

        public bool Delete(int id)
        {
            var existingProjectById = _projectRepository.Get(id);

            if (existingProjectById != null)
            {
                _projectRepository.Remove(existingProjectById);
                return true;
            }
            return false;
        }

        public bool Edit(int id, Project project)
        {
            var existingProjectById = _projectRepository.Get(id);

            if (existingProjectById == null || project == null) return false;

            existingProjectById.ImagePath = project.ImagePath;
            existingProjectById.Title = project.Title;
            existingProjectById.Description = project.Description;

            _projectRepository.Update(existingProjectById);

            return true;
        }

        public IQueryable<Project>? GetAll()
        {
            return _projectRepository.GetAll();
        }
    }
}