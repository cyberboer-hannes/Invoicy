using Invoicy.Application.Customers.Models;

namespace Invoicy.Application.Services.Customers.Interfaces;

public interface ICustomerService
{
    Task AddCustomerAsync(CustomerModel model);
    Task<CustomerModel?> GetCustomerByIdAsync(int id);
    Task<List<CustomerModel>> GetAllCustomersAsync();
    Task UpdateCustomerAsync(CustomerModel model);
}
