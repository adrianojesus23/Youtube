using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseDadosConnections.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int VendorID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal PaymentTotal { get; set; }
        public decimal CreditTotal { get; set; }
        public int TermsID { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        // Navigation property to Vendor
        public Vendor Vendor { get; set; }
    }

}
