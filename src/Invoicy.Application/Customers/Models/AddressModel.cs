using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Customers.Models;

public class AddressModel
{
    public int Id { get; set; }
    public required string Province { get; set; }
    public required string City { get; set; }
    public string? Suburb { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }
}
