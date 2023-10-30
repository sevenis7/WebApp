namespace DataLayer.Interfaces
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<T?> Get(int id);
        Task<IEnumerable<T>> All();
        Task Update(T entity);
    }
}
