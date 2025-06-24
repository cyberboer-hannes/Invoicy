using System.IO;
using System.Reflection.Emit;

namespace Invoicy.Domain.Customers;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string TelephoneNumber { get; private set; } = string.Empty;

    public int AddressId { get; private set; }
    public Address Address { get; private set; } = null!;

    private Customer() { }

    public static Customer Create(string name, string telephoneNumber, Address address)
    {
        return new Customer
        {
            Name = name,
            TelephoneNumber = telephoneNumber,
            Address = address,
            AddressId = address.Id 
        };
    }

    public void Update(string name, string telephoneNumber, string province, string city, string? suburb, string street, string zipCode)
    {
        Name = name;
        TelephoneNumber = telephoneNumber;

        if (Address != null)
        {
            Address.Update(province, city, suburb, street, zipCode);
        }
        else
        {
            Address = new Address(province, city, suburb, street, zipCode);
        }
    }
}
