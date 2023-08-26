using DAL.Context;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {

        protected readonly KeyPassContext _keyPassContext;


        public BaseRepository(KeyPassContext keyPassContext)
        {
            this._keyPassContext = keyPassContext;
        }

        private DbSet<T> entities;
        protected DbSet<T> Entities => entities ??= _keyPassContext.Set<T>();



        public virtual async Task<T?> CreateAsync(T entity)
        {
            try
            {
                var entry = _keyPassContext.Add(entity);
                await this.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entities
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
            => await Entities
                .FirstOrDefaultAsync(predicate);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await Entities.ToListAsync();

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            Entities.RemoveRange(Entities.Where(predicate));
            await this.SaveChangesAsync();
        }
        
        public virtual async Task DeleteAllAsync()
        {
            Entities.RemoveRange(Entities);
            await this.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _keyPassContext.SaveChangesAsync();
        }
    }
}
