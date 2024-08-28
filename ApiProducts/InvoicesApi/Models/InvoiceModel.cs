namespace InvoicesApi;

public class InvoiceModel
{
    protected int InvoiceId { get; init; }
    public int VendorId { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal InvoiceTotal { get; set; }
    public decimal PaymentTotal { get; set; }
    public decimal CreditTotal { get; set; }
    public int TermsId { get; set; }
    public DateTime InvoiceDueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public VendorModel Vendor { get; set; }
}