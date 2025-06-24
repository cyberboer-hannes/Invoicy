using Invoicy.Application.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Invoices.Models;

public class InvoiceResponseModel
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public CustomerModel Customer { get; set; } = null!;
    public List<InvoiceDetailModel> InvoiceDetails { get; set; } = [];
}
