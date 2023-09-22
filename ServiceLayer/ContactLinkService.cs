using DataLayer.Entities;
using DataLayer.Interface;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ContactLinkService : IBaseService<ContactLink>
    {
        private readonly IBaseRepository<ContactLink> _contactLinkRepository;

        public ContactLinkService(IBaseRepository<ContactLink> contactLinkRepository)
        {
            _contactLinkRepository = contactLinkRepository;
        }

        public ContactLink? Create(ContactLink entity)
        {
            _contactLinkRepository.Add(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var existingLinkById = _contactLinkRepository.Get(id);

            if (existingLinkById != null)
            {
                _contactLinkRepository.Remove(existingLinkById);
                return true;
            }

            return false;
        }

        public bool Edit(int id, ContactLink entity)
        {
            var existingLinkById = _contactLinkRepository.Get(id);

            if (existingLinkById == null || entity == null) return false;

            existingLinkById.Url = entity.Url;

            _contactLinkRepository.Update(existingLinkById);

            return true;
        }

        public IQueryable<ContactLink>? GetAll()
        {
            return _contactLinkRepository.GetAll();
        }
    }
}
