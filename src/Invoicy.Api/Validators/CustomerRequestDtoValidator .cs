using FluentValidation;
using Invoicy.Api.DTOs;

namespace Invoicy.Api.Validators;

public class CustomerRequestDtoValidator : AbstractValidator<CustomerRequestDto>
{
    public CustomerRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TelephoneNumber).NotEmpty();
        RuleFor(x => x.Address).NotNull();
    }
}
