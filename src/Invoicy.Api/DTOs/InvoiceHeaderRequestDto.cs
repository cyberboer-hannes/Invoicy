namespace Invoicy.Api.DTOs;
public class InvoiceHeaderRequestDto
{
    public required CustomerRequestDto Customer { get; set; } 
    public required List<InvoiceDetailRequestDto> InvoiceDetails { get; set; }
}
