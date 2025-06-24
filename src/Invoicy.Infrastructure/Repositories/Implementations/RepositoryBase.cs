using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Invoicy.Infrastructure.Repositories.Implementations;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public Task DeleteAsync(T entity) { _context.Set<T>().Remove(entity); return Task.CompletedTask; }
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _context.Set<T>().Where(predicate).ToListAsync();
}
