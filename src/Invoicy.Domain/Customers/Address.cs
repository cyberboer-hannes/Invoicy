namespace Invoicy.Domain.Customers;

public class Address
{
    public int Id { get; private set; }
    public string Province { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string? Suburb { get; private set; }
    public string Street { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;

    private Address() { }

    public Address(string province, string city, string? suburb, string street, string zipCode)
    {
        Province = province;
        City = city;
        Suburb = suburb;
        Street = street;
        ZipCode = zipCode;
    }
    public void Update(string province, string city, string? suburb, string street, string zipCode)
    {
        Province = province;
        City = city;
        Suburb = suburb;
        Street = street;
        ZipCode = zipCode;
    }
}
