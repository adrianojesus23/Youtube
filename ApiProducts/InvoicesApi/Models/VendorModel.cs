namespace InvoicesApi;

public class VendorModel
{
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public string VendorAddress1 { get; set; }
    public string VendorAddress2 { get; set; }
    public string VendorCity { get; set; }
    public string VendorState { get; set; }
    public string VendorZipCode { get; set; }
    public string VendorPhone { get; set; }
    public string VendorContactLName { get; set; }
    public string VendorContactFName { get; set; }
    public int DefaultTermsId { get; set; }
    public int DefaultAccountNo { get; set; }
    public IList<InvoiceModel> Invoices { get; set; }
}