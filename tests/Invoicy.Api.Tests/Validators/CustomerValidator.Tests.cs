using FluentAssertions;
using Invoicy.Api.DTOs;
using Invoicy.Api.Validators;
using Xunit;

namespace Invoicy.Api.Tests.Validators;

public class CustomerValidatorTests
{
    [Fact]
    public void ShouldPassWhenValidCustomer()
    {
        var validator = new CustomerRequestDtoValidator();
        var dto = new CustomerRequestDto
        {
            Name = "John Doe",
            TelephoneNumber = "123456789",
            Address = new AddressRequestDto
            {
                Province = "Province",
                City = "City",
                Suburb = null,
                Street = "Street",
                ZipCode = "Zip"
            }
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailWhenNameIsEmpty()
    {
        var validator = new CustomerRequestDtoValidator();
        var dto = new CustomerRequestDto
        {
            Name = "",
            TelephoneNumber = "123456789",
            Address = new AddressRequestDto
            {
                Province = "Province",
                City = "City",
                Suburb = null,
                Street = "Street",
                ZipCode = "Zip"
            }
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void ShouldFailWhenTelephoneIsEmpty()
    {
        var validator = new CustomerRequestDtoValidator();
        var dto = new CustomerRequestDto
        {
            Name = "John Doe",
            TelephoneNumber = "",
            Address = new AddressRequestDto
            {
                Province = "Province",
                City = "City",
                Suburb = null,
                Street = "Street",
                ZipCode = "Zip"
            }
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "TelephoneNumber");
    }

    [Fact]
    public void ShouldFailWhenAddressIsNull()
    {
        var validator = new CustomerRequestDtoValidator();
        var dto = new CustomerRequestDto
        {
            Name = "John Doe",
            TelephoneNumber = "123456789",
            Address = null!
        };

        var result = validator.Validate(dto);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Address");
    }
}
