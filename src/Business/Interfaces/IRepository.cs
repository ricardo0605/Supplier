using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveChangesAsync();
    }
}
