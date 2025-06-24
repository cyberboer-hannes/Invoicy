namespace Invoicy.Application.Services.Generators.Interfaces;

public interface IInvoiceNumberGenerator
{
    Task<string> GenerateNextInvoiceNumberAsync();
}
