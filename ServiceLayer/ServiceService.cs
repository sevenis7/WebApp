using DataLayer.Entities;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ServiceService : IService<Service>
    {
        private readonly IRepository<Service> _serviceRepository;

        public ServiceService(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<Service>> All() => await _serviceRepository.All();

        public async Task<Service?> Delete(int id)
        {
            var existingServiceById = await Get(id);

            if (existingServiceById == null) return null;

            await _serviceRepository.Delete(existingServiceById);

            return existingServiceById;
        }

        public async Task<Service?> Edit(int id, Service entity)
        {
            var existingServiceById = await Get(id);

            if(existingServiceById == null || entity == null) return null;

            existingServiceById.Title = entity.Title;
            existingServiceById.Text = entity.Text;

            await _serviceRepository.Update(existingServiceById);

            return existingServiceById;
        }

        public async Task<Service?> Get(int id) => await _serviceRepository.Get(id);

        public async Task<Service?> Post(Service entity)
        {
            if (entity == null) return null;

            await _serviceRepository.Add(entity);

            return entity;
        }
    }
}
