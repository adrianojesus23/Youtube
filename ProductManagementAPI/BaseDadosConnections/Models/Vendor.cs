using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDadosConnections.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress1 { get; set; }
        public string VendorAddress2 { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZipCode { get; set; }
        public string VendorPhone { get; set; }
        public string VendorContactLName { get; set; }
        public string VendorContactFName { get; set; }
        public int DefaultTermsID { get; set; }
        public string DefaultAccountNo { get; set; }

        // Navigation property to Invoices
        public ICollection<Invoice> Invoices { get; set; }
    }

}
