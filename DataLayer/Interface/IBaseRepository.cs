using DataLayer.Entities;

namespace DataLayer.Interface
{
    public interface IBaseRepository<T>
    {
        T? Get(int id);

        T Add(T entity);

        IQueryable<T>? GetAll();

        void Update(T entity);

        void Remove(T entity);
    }
}
