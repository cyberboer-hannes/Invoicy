namespace Invoicy.Application.Customers.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string TelephoneNumber { get; set; }
    public required AddressModel Address { get; set; }
}
