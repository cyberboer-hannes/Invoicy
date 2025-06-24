using FluentAssertions;
using Invoicy.Domain.Customers;
using Invoicy.Domain.Invoices;
using Invoicy.Infrastructure.Persistence;
using Invoicy.Infrastructure.Repositories.Implementations;
using Invoicy.Infrastructure.Services.Generators.Implementations;
using Invoicy.Api.Tests.Fakes;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Invoicy.Api.Tests.Generators;

public class InvoiceNumberGeneratorTests
{
    [Fact]
    public async Task ShouldGenerateSequentialInvoiceNumbers()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb_Generator")
            .Options;

        using var context = new ApplicationDbContext(options);
        var generator = new InvoiceNumberGenerator(context);

        var expectedNumbers = new List<string>
        {
            "INV0000000001",
            "INV0000000002",
            "INV0000000003",
            "INV0000000004",
            "INV0000000005"
        };

        var actualNumbers = new List<string>();

        var fixedNow = new DateTime(2025, 6, 20, 12, 0, 0);
        var dateTimeProvider = new FakeDateTimeProvider { Now = fixedNow };

        for (int i = 0; i < 5; i++)
        {
            var nextNumber = await generator.GenerateNextInvoiceNumberAsync();

            var customer = Customer.Create("Test Customer", "000", new Address("Province", "City", null, "Street", "ZipCode"));
            var invoice = InvoiceHeader.Create(nextNumber, customer, dateTimeProvider.Now);

            context.Customers.Add(customer);
            context.Invoices.Add(invoice);
            await context.SaveChangesAsync();

            actualNumbers.Add(nextNumber);
        }

        actualNumbers.Should().BeEquivalentTo(expectedNumbers);
    }
}
