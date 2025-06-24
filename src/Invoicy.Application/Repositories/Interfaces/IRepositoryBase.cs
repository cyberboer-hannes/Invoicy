using System.Linq.Expressions;

namespace Invoicy.Application.Repositories.Interfaces;

public interface IRepositoryBase<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
}