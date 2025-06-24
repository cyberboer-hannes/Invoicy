namespace Invoicy.Api.DTOs;

public class UpdateAddressRequestDto
{
    public int Id { get; set; } 
    public required string Province { get; set; } = string.Empty;
    public required string City { get; set; } = string.Empty;
    public string? Suburb { get; set; }
    public required string Street { get; set; } = string.Empty;
    public required string ZipCode { get; set; } = string.Empty;
}
