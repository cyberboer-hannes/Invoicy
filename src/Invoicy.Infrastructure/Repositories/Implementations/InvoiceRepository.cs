using Invoicy.Api.Repositories.Interfaces;
using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Domain.Invoices;
using Invoicy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Invoicy.Infrastructure.Repositories.Implementations;

public class InvoiceRepository : RepositoryBase<InvoiceHeader>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context) { }
    public async Task<InvoiceHeader?> GetByIdWithCustomerAndAddressAsync(int id)
    {
        return await _context.Invoices
            .Include(i => i.Customer)
            .ThenInclude(c => c.Address)
            .Include(i => i.InvoiceDetails)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<List<InvoiceHeader>> GetAllWithCustomerAndAddressAsync()
    {
        return await _context.Invoices
            .Include(i => i.Customer)
                .ThenInclude(c => c.Address)
            .ToListAsync();
    }
}
