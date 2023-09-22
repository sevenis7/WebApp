using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IBaseService<T>
    {
        T? Create(T entity);

        bool Delete(int id);

        IQueryable<T>? GetAll();

        bool Edit(int id, T entity);
    }
}
