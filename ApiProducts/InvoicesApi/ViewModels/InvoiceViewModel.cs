using System.ComponentModel.DataAnnotations;

namespace InvoicesApi;

public class InvoiceViewModel
{
    [Required]
    public int VendorId { get; set; }
    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; }
    [Required]
    public DateTime InvoiceDate { get; set; }
    public decimal InvoiceTotal { get; set; }
    public int TermsId { get; set; }
    public DateTime InvoiceDueDate { get; set; }
}
