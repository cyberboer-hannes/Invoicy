using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Domain.Invoices;

namespace Invoicy.Api.Repositories.Interfaces;

public interface IInvoiceRepository : IRepositoryBase<InvoiceHeader> 
{
    Task<InvoiceHeader?> GetByIdWithCustomerAndAddressAsync(int id);
    Task<List<InvoiceHeader>> GetAllWithCustomerAndAddressAsync();
}

