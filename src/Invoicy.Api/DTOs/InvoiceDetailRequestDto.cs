namespace Invoicy.Api.DTOs
{
    public class InvoiceDetailRequestDto
    {
        public required string ItemDescription { get; set; } = string.Empty;
        public required decimal Quantity { get; set; }
        public required decimal UnitPrice { get; set; }

    }
}
