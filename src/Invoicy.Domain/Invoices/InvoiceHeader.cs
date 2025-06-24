using Invoicy.Domain.Customers;

namespace Invoicy.Domain.Invoices;

public class InvoiceHeader
{
    public int Id { get; init; }
    public string InvoiceNumber { get; private set; } = string.Empty;
    public DateTime InvoiceDate { get; private set; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public List<InvoiceDetail> InvoiceDetails { get; set; } = [];

    private InvoiceHeader() { }
    public static InvoiceHeader Create(string invoiceNumber, Customer customer, DateTime invoiceDate)
    {
        return new InvoiceHeader
        {
            InvoiceNumber = invoiceNumber,
            InvoiceDate = invoiceDate,
            CustomerId = customer.Id,
            Customer = customer,
            InvoiceDetails = []
        };
    }
    public void AddDetail(string itemDescription, decimal quantity, decimal unitPrice)
    {
        var detail = InvoiceDetail.Create(itemDescription, quantity, unitPrice);
        InvoiceDetails.Add(detail);
    }
    public void ClearDetails()
    {
        InvoiceDetails.Clear();
    }
}
