using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> All();
        Task<T?> Delete(int id);
        Task<T?> Edit(int id, T entity);
        Task<T?> Get(int id);
        Task<T?> Post(T entity);
    }
}
