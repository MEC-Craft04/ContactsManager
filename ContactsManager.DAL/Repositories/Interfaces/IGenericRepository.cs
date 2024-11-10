namespace ContactsManager.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable GetAll();
        Task<T?> GetById(int id);
        Task<T> Create(T entity);
        T Update(T entity);
        Task<bool> Delete(int id);
    }
}
