namespace Invoicy.Api.DTOs;

public class InvoiceDetailResponseDto
{
    public required string ItemDescription { get; set; } = string.Empty;
    public required decimal Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity * UnitPrice;
}
