using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ContactsDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ContactsDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public virtual IQueryable GetAll() => _dbSet;

        public virtual async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);

        public virtual async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public virtual async Task<bool> Delete(int id)
        {
            T? result = await GetById(id);

            if (result == null)
                return false;

            _dbSet.Remove(result);
            return true;
        }
    }
}
