namespace Invoicy.Api.DTOs
{
    public class UpdateCustomerRequestDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string TelephoneNumber { get; set; } = string.Empty;
        public required UpdateAddressRequestDto Address { get; set; } = default!;

    }
}
