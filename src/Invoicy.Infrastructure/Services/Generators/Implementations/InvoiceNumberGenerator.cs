using Invoicy.Application.Services.Generators.Interfaces;
using Invoicy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Invoicy.Infrastructure.Services.Generators.Implementations;

public class InvoiceNumberGenerator : IInvoiceNumberGenerator
{
    private readonly ApplicationDbContext _context;

    public InvoiceNumberGenerator(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateNextInvoiceNumberAsync()
    {
        var lastInvoice = await _context.Invoices
            .OrderByDescending(i => i.Id)
            .FirstOrDefaultAsync();

        var lastNumber = lastInvoice == null
            ? 0
            : int.Parse(lastInvoice.InvoiceNumber.Substring(3));

        var nextNumber = lastNumber + 1;
        return $"INV{nextNumber.ToString().PadLeft(10, '0')}";
    }
}
