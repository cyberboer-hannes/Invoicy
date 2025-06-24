using Invoicy.Application.Customers.Models;
using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Application.Services.Customers.Interfaces;
using Invoicy.Domain.Customers;

namespace Invoicy.Application.Services.Customers.Implementations;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddCustomerAsync(CustomerModel model)
    {
        await EnsurePhoneNumberIsUniqueAsync(model.TelephoneNumber);

        var address = new Address(
            model.Address.Province,
            model.Address.City,
            model.Address.Suburb,
            model.Address.Street,
            model.Address.ZipCode
        );

        var customer = Customer.Create(
            model.Name,
            model.TelephoneNumber,
            address
        );

        await _customerRepository.AddAsync(customer);
        await _unitOfWork.CommitAsync();
    }

    public async Task<CustomerModel?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdWithAddressAsync(id);
        if (customer == null) return null;

        return MapToCustomerModel(customer);
    }

    public async Task<List<CustomerModel>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllWithAddressAsync();
        return customers.Select(MapToCustomerModel).ToList();
    }

    public async Task UpdateCustomerAsync(CustomerModel model)
    {
        var customer = await _customerRepository.GetByIdWithAddressAsync(model.Id);
        if (customer == null)
            throw new Exception($"Customer with ID {model.Id} not found.");

        var duplicateCustomer = await _customerRepository
            .FindByTelephoneNumberExcludingIdAsync(model.TelephoneNumber, model.Id);

        if (duplicateCustomer != null)
            throw new Exception("Another customer with this telephone number already exists.");

        customer.Update(
            model.Name,
            model.TelephoneNumber,
            model.Address.Province,
            model.Address.City,
            model.Address.Suburb,
            model.Address.Street,
            model.Address.ZipCode
        );

        await _unitOfWork.CommitAsync();
    }

    private async Task EnsurePhoneNumberIsUniqueAsync(string telephoneNumber, int? currentCustomerId = null)
    {
        var existing = currentCustomerId.HasValue
            ? await _customerRepository.FindByTelephoneNumberExcludingIdAsync(telephoneNumber, currentCustomerId.Value)
            : await _customerRepository.FindByTelephoneNumberAsync(telephoneNumber);

        if (existing != null)
            throw new Exception("A customer with this telephone number already exists.");
    }

    private static CustomerModel MapToCustomerModel(Customer customer)
    {
        return new CustomerModel
        {
            Id = customer.Id,
            Name = customer.Name,
            TelephoneNumber = customer.TelephoneNumber,
            Address = new AddressModel
            {
                Id = customer.Address.Id,
                Province = customer.Address.Province,
                City = customer.Address.City,
                Suburb = customer.Address.Suburb,
                Street = customer.Address.Street,
                ZipCode = customer.Address.ZipCode
            }
        };
    }
}
