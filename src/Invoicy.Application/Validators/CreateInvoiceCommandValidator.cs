using FluentValidation;
using Invoicy.Application.Abstractions.Clock;
using Invoicy.Application.Invoices.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Validators;

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull().WithMessage("Customer information is required.");

        When(x => x.Customer != null, () =>
        {
            RuleFor(x => x.Customer.Name)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(x => x.Customer.TelephoneNumber)
                .NotEmpty().WithMessage("Customer telephone number is required.");

            RuleFor(x => x.Customer.Address)
                .NotNull().WithMessage("Customer address is required.");

            When(x => x.Customer.Address != null, () =>
            {
                RuleFor(x => x.Customer.Address.Province).NotEmpty();
                RuleFor(x => x.Customer.Address.City).NotEmpty();
                RuleFor(x => x.Customer.Address.Street).NotEmpty();
                RuleFor(x => x.Customer.Address.ZipCode).NotEmpty();
            });
        });

        RuleFor(x => x.InvoiceDetails)
            .NotEmpty().WithMessage("At least one invoice detail is required.");
    }
}
