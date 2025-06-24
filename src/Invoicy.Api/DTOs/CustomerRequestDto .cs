namespace Invoicy.Api.DTOs
{
    public class CustomerRequestDto
    {
        public required string Name { get; set; } = string.Empty;
        public required string TelephoneNumber { get; set; } = string.Empty;
        public required AddressRequestDto Address { get; set; } = default!;
    }
}
