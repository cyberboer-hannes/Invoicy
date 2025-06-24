using FluentAssertions;
using Invoicy.Api.DTOs;
using Invoicy.Api.Validators;
using Xunit;

namespace Invoicy.Api.Tests.Validators;

public class InvoiceHeaderRequestDtoValidatorTests
{
    [Fact]
    public void ShouldPassWhenValid()
    {
        var validator = new InvoiceHeaderRequestDtoValidator();

        var dto = new InvoiceHeaderRequestDto
        {
            Customer = new CustomerRequestDto
            {
                Name = "John Doe",
                TelephoneNumber = "123456789",
                Address = new AddressRequestDto
                {
                    Province = "Province",
                    City = "City",
                    Suburb = null,
                    Street = "Street",
                    ZipCode = "ZipCode"
                }
            },
            InvoiceDetails = new List<InvoiceDetailRequestDto>
            {
                new InvoiceDetailRequestDto
                {
                    ItemDescription = "Item 1",
                    Quantity = 2,
                    UnitPrice = 100
                }
            }
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailWhenCustomerIsNull()
    {
        var validator = new InvoiceHeaderRequestDtoValidator();

        var dto = new InvoiceHeaderRequestDto
        {
            Customer = null!,
            InvoiceDetails = new List<InvoiceDetailRequestDto>
            {
                new InvoiceDetailRequestDto
                {
                    ItemDescription = "Item 1",
                    Quantity = 2,
                    UnitPrice = 100
                }
            }
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Customer");
    }

    [Fact]
    public void ShouldFailWhenInvoiceDetailsIsEmpty()
    {
        var validator = new InvoiceHeaderRequestDtoValidator();

        var dto = new InvoiceHeaderRequestDto
        {
            Customer = new CustomerRequestDto
            {
                Name = "John Doe",
                TelephoneNumber = "123456789",
                Address = new AddressRequestDto
                {
                    Province = "Province",
                    City = "City",
                    Suburb = null,
                    Street = "Street",
                    ZipCode = "ZipCode"
                }
            },
            InvoiceDetails = new List<InvoiceDetailRequestDto>()  // empty list
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "InvoiceDetails");
    }
}
