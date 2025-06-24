using Invoicy.Application.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Invoices.Models
{
    public class InvoiceRequestModel
    {
        public required CustomerModel Customer { get; set; }
        public required List<InvoiceDetailModel> InvoiceDetails { get; set; }
    }
}
