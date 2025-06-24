using Invoicy.Api.DTOs;
using FluentValidation;

namespace Invoicy.Api.Validators;

public class InvoiceHeaderRequestDtoValidator : AbstractValidator<InvoiceHeaderRequestDto>
{
    public InvoiceHeaderRequestDtoValidator()
    {
        RuleFor(x => x.Customer).NotNull();
        RuleFor(x => x.InvoiceDetails).NotEmpty();
    }
}
