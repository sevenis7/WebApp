using DataLayer.Entities;
using DataLayer.Interface;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ServiceSerivce : IBaseService<Service>
    {
        private readonly IBaseRepository<Service> _serviceRepository;

        public ServiceSerivce(IBaseRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Service? Create(Service service)
        {
            _serviceRepository.Add(service);

            return service;
        }

        public bool Delete(int id)
        {
            var existingServiceById = _serviceRepository.Get(id);

            if (existingServiceById != null)
            {
                _serviceRepository.Remove(existingServiceById);
                return true;
            }

            return false;
        }

        public bool Edit(int id, Service service)
        {
            var existingServiceById = _serviceRepository.Get(id);

            if (existingServiceById == null || service == null) return false;

            existingServiceById.Title = service.Title;
            existingServiceById.Description = service.Description;

            _serviceRepository.Update(existingServiceById);

            return true;
        }

        public IQueryable<Service>? GetAll()
        {
            return _serviceRepository.GetAll();
        }
    }
}
