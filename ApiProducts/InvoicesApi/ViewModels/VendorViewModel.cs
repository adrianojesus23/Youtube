using System.ComponentModel.DataAnnotations;

namespace InvoicesApi;

public class VendorViewModel
{
    [Required]
    public string VendorName { get; set; }
    [Required]
    public string VendorCity { get; set; }
    [Required]
    public string VendorState { get; set; }
    [Required]
    public string VendorZipCode { get; set; }

    public int DefaultTermsId { get; set; } = 1;
    public int DefaultAccountNo { get; set; } = 533;
}
