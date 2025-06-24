using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Domain.Customers;
using Invoicy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Invoicy.Infrastructure.Repositories.Implementations;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Customer?> FindByTelephoneNumberAsync(string telephoneNumber)
    {
        return await _context.Customers
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.TelephoneNumber == telephoneNumber);
    }

    public async Task<Customer?> FindByTelephoneNumberExcludingIdAsync(string telephoneNumber, int excludeCustomerId)
    {
        return await _context.Customers
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.TelephoneNumber == telephoneNumber && c.Id != excludeCustomerId);
    }

    public async Task<List<Customer>> GetAllWithAddressAsync()
    {
        return await _context.Customers
            .Include(c => c.Address)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdWithAddressAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
