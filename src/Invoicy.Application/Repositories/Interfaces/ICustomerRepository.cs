using Invoicy.Domain.Customers;

namespace Invoicy.Application.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer?> FindByTelephoneNumberAsync(string telephoneNumber);
    Task<Customer?> FindByTelephoneNumberExcludingIdAsync(string telephoneNumber, int excludeCustomerId);
    Task<List<Customer>> GetAllWithAddressAsync();
    Task<Customer?> GetByIdWithAddressAsync(int id);
}
