using FluentAssertions;
using Invoicy.Application.Abstractions.Clock;
using Invoicy.Application.Invoices.Commands;
using Invoicy.Application.Invoices.Models;
using Invoicy.Application.Services.Generators.Interfaces;
using Invoicy.Application.Services.Invoices.Implementations;
using Invoicy.Application.Repositories.Interfaces;
using Invoicy.Domain.Customers;
using Invoicy.Domain.Invoices;
using Invoicy.Infrastructure.Persistence;
using Invoicy.Infrastructure.Repositories.Implementations;
using Invoicy.Infrastructure.Services.Generators.Implementations;
using Invoicy.Api.Tests.Fakes;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Invoicy.Application.Customers.Models;

namespace Invoicy.Api.Tests.Services;

public class InvoiceServiceTests
{
    [Fact]
    public async Task ShouldCreateInvoiceCorrectly()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb2")
            .Options;

        await using var context = new ApplicationDbContext(options);
        var customerRepo = new CustomerRepository(context);
        var invoiceRepo = new InvoiceRepository(context);
        var generator = new InvoiceNumberGenerator(context);
        var unitOfWork = new UnitOfWork(context);
        var dateTimeProvider = new FakeDateTimeProvider { Now = new DateTime(2025, 6, 20, 12, 0, 0) };

        var service = new InvoiceService(invoiceRepo, customerRepo, generator, dateTimeProvider, unitOfWork);

        var command = new CreateInvoiceCommand
        {
            Customer = new CustomerModel
            {
                Name = "Jane Doe",
                TelephoneNumber = "987654321",
                Address = new AddressModel
                {
                    Province = "Province",
                    City = "City",
                    Suburb = null,
                    Street = "Street",
                    ZipCode = "ZipCode"
                }
            },
            InvoiceDetails = new List<InvoiceDetailModel>
            {
                new InvoiceDetailModel
                {
                    ItemDescription = "Product 1",
                    Quantity = 2,
                    UnitPrice = 100
                }
            }
        };

        await service.CreateInvoiceAsync(command);

        (await context.Invoices.CountAsync()).Should().Be(1);
        (await context.InvoiceDetails.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task ShouldRetrieveInvoiceCorrectly()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_Retrieve")
            .Options;

        await using var context = new ApplicationDbContext(options);
        var customerRepo = new CustomerRepository(context);
        var invoiceRepo = new InvoiceRepository(context);
        var generator = new InvoiceNumberGenerator(context);
        var unitOfWork = new UnitOfWork(context);
        var dateTimeProvider = new FakeDateTimeProvider { Now = new DateTime(2025, 6, 20, 12, 0, 0) };

        var service = new InvoiceService(invoiceRepo, customerRepo, generator, dateTimeProvider, unitOfWork);

        var command = new CreateInvoiceCommand
        {
            Customer = new CustomerModel
            {
                Name = "Test Customer",
                TelephoneNumber = "123",
                Address = new AddressModel
                {
                    Province = "P",
                    City = "C",
                    Suburb = null,
                    Street = "S",
                    ZipCode = "Z"
                }
            },
            InvoiceDetails = new List<InvoiceDetailModel>
            {
                new InvoiceDetailModel
                {
                    ItemDescription = "Item 1",
                    Quantity = 1,
                    UnitPrice = 10
                }
            }
        };

        await service.CreateInvoiceAsync(command);

        var invoice = await service.GetInvoiceByIdAsync(1);

        invoice.Should().NotBeNull();
        invoice!.Customer.Name.Should().Be("Test Customer");
        invoice.InvoiceDetails.Should().HaveCount(1);
    }

    [Fact]
    public async Task ShouldReturnNullWhenInvoiceNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_NotFound")
            .Options;

        await using var context = new ApplicationDbContext(options);
        var customerRepo = new CustomerRepository(context);
        var invoiceRepo = new InvoiceRepository(context);
        var generator = new InvoiceNumberGenerator(context);
        var unitOfWork = new UnitOfWork(context);
        var dateTimeProvider = new FakeDateTimeProvider { Now = new DateTime(2025, 6, 20, 12, 0, 0) };

        var service = new InvoiceService(invoiceRepo, customerRepo, generator, dateTimeProvider, unitOfWork);

        var result = await service.GetInvoiceByIdAsync(99);
        result.Should().BeNull();
    }
}
