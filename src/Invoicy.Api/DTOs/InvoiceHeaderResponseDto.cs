namespace Invoicy.Api.DTOs;

public class InvoiceHeaderResponseDto
{
    public required string InvoiceNumber { get; set; } = string.Empty;
    public required DateTime InvoiceDate { get; set; }
    public required CustomerResponseDto Customer { get; set; }
    public required List<InvoiceDetailResponseDto> InvoiceDetails { get; set; }
}
