﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        public Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
        
        public Task<T?> CreateAsync(T entity);
        public Task DeleteAsync(Expression<Func<T, bool>> predicate);
        public Task DeleteAllAsync();

        public Task<T?> UpdateAsync(int id, T entity);

        Task SaveChangesAsync();






    }
}
