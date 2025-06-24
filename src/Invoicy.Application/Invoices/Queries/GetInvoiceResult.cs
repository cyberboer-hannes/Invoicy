using Invoicy.Application.Customers.Models;
using Invoicy.Application.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Invoices.Queries
{
    public class GetInvoiceResult
    {
        public int Id { get; set; }
        public required string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public required CustomerModel Customer { get; set; }
        public required List<InvoiceDetailModel> InvoiceDetails { get; set; }
    }
}
