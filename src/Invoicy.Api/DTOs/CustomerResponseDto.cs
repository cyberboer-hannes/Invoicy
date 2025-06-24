namespace Invoicy.Api.DTOs;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TelephoneNumber { get; set; } = string.Empty;
    public AddressResponseDto Address { get; set; } = default!;
}
