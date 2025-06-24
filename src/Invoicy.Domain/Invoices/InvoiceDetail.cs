namespace Invoicy.Domain.Invoices;

public class InvoiceDetail
{
    public int Id { get; init; }
    public string ItemDescription { get; private set; } = string.Empty;
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal LineTotal => Quantity * UnitPrice;

    public int InvoiceHeaderId { get; private set; }
    public InvoiceHeader InvoiceHeader { get; private set; } = null!;

    private InvoiceDetail() { }

    public static InvoiceDetail Create(string itemDescription, decimal quantity, decimal unitPrice)
    {
        return new InvoiceDetail
        {
            ItemDescription = itemDescription,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
    public void Update(string itemDescription, decimal quantity, decimal unitPrice)
    {
        ItemDescription = itemDescription;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
