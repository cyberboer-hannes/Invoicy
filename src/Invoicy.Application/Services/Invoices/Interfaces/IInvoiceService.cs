using Invoicy.Application.Invoices.Commands;
using Invoicy.Application.Invoices.Queries;

namespace Invoicy.Application.Services.Invoices.Interfaces;

public interface IInvoiceService
{
    Task CreateInvoiceAsync(CreateInvoiceCommand command);
    Task<GetInvoiceResult?> GetInvoiceByIdAsync(int id);
    Task<List<GetInvoiceResult>> GetAllInvoicesAsync();
}