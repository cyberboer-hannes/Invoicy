using Invoicy.Api.DTOs;
using FluentValidation;

namespace Invoicy.Api.Validators;

public class CustomerResponseDtoValidator : AbstractValidator<CustomerResponseDto>
{
    public CustomerResponseDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TelephoneNumber).NotEmpty();
        RuleFor(x => x.Address).NotNull();
    }
}