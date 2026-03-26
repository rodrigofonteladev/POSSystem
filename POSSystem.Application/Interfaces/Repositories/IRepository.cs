using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace POSSystem.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null
        );
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task<T?> GetByIdAsync(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}