using Invoicy.Api.Repositories.Interfaces;
using Invoicy.Application.Abstractions.Clock;
using Invoicy.Application.Customers.Models;
using Invoicy.Application.Invoices.Commands;
using Invoicy.Application.Invoices.Models;
using Invoicy.Application.Invoices.Queries;
using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Application.Services.Generators.Interfaces;
using Invoicy.Application.Services.Invoices.Interfaces;
using Invoicy.Domain.Customers;
using Invoicy.Domain.Invoices;

namespace Invoicy.Application.Services.Invoices.Implementations;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInvoiceNumberGenerator _invoiceNumberGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository,
        IInvoiceNumberGenerator invoiceNumberGenerator,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _customerRepository = customerRepository;
        _invoiceNumberGenerator = invoiceNumberGenerator;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateInvoiceAsync(CreateInvoiceCommand command)
    {
        // Look for customer by telephone number
        var existingCustomer = await _customerRepository.FindByTelephoneNumberAsync(command.Customer.TelephoneNumber);
        Customer customer;

        if (existingCustomer != null)
        {
            // Update existing customer with latest data
            existingCustomer.Update(
                command.Customer.Name,
                command.Customer.TelephoneNumber,
                command.Customer.Address.Province,
                command.Customer.Address.City,
                command.Customer.Address.Suburb,
                command.Customer.Address.Street,
                command.Customer.Address.ZipCode
            );

            await _unitOfWork.CommitAsync(); // commit customer update
            customer = existingCustomer;
        }
        else
        {
            // Create new customer
            var address = new Address(
                command.Customer.Address.Province,
                command.Customer.Address.City,
                command.Customer.Address.Suburb,
                command.Customer.Address.Street,
                command.Customer.Address.ZipCode
            );

            customer = Customer.Create(
                command.Customer.Name,
                command.Customer.TelephoneNumber,
                address
            );

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync(); // commit new customer
        }

        // Generate invoice
        var invoiceNumber = await _invoiceNumberGenerator.GenerateNextInvoiceNumberAsync();
        var invoice = InvoiceHeader.Create(invoiceNumber, customer, _dateTimeProvider.Now);

        foreach (var detail in command.InvoiceDetails)
        {
            invoice.InvoiceDetails.Add(InvoiceDetail.Create(
                detail.ItemDescription,
                detail.Quantity,
                detail.UnitPrice));
        }

        await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.CommitAsync();
    }


    public async Task<GetInvoiceResult?> GetInvoiceByIdAsync(int id)
    {
        var invoice = await _invoiceRepository.GetByIdWithCustomerAndAddressAsync(id);
        if (invoice == null) return null;

        return new GetInvoiceResult
        {
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            Customer = new CustomerModel
            {
                Id = invoice.Customer.Id,
                Name = invoice.Customer.Name,
                TelephoneNumber = invoice.Customer.TelephoneNumber,
                Address = new AddressModel
                {
                    Id = invoice.Customer.Address.Id,
                    Province = invoice.Customer.Address.Province,
                    City = invoice.Customer.Address.City,
                    Suburb = invoice.Customer.Address.Suburb,
                    Street = invoice.Customer.Address.Street,
                    ZipCode = invoice.Customer.Address.ZipCode
                }
            },
            InvoiceDetails = invoice.InvoiceDetails.Select(d => new InvoiceDetailModel
            {
                ItemDescription = d.ItemDescription,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        };
    }

    public async Task<List<GetInvoiceResult>> GetAllInvoicesAsync()
    {
        var invoices = await _invoiceRepository.GetAllWithCustomerAndAddressAsync();

        return invoices.Select(invoice => new GetInvoiceResult
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            Customer = MapToCustomerModel(invoice.Customer),
            InvoiceDetails = [] // no details on list page
        }).ToList();
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
