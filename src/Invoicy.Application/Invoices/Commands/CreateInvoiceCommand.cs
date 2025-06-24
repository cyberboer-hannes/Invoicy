using Invoicy.Application.Customers.Models;
using Invoicy.Application.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Invoices.Commands
{
    public class CreateInvoiceCommand
    {
        public CustomerModel Customer { get; set; } = default!;
        public List<InvoiceDetailModel> InvoiceDetails { get; set; } = [];
    }
}
