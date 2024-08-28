namespace InvoicesApi;

public static class ExtensionsInvoices
{
    public static InvoiceViewModel ToViewModel(this InvoiceModel model)
    {
        return new InvoiceViewModel
        {
            VendorId = model.VendorId,
            InvoiceNumber = model.InvoiceNumber,
            InvoiceDate = model.InvoiceDate,
            InvoiceTotal = model.InvoiceTotal,
            TermsId = model.TermsId,
            InvoiceDueDate = model.InvoiceDueDate
        };
    }

    public static InvoiceModel ToModel(this InvoiceViewModel viewModel)
    {
        return new InvoiceModel
        {
            VendorId = viewModel.VendorId,
            InvoiceNumber = viewModel.InvoiceNumber,
            InvoiceDate = viewModel.InvoiceDate,
            InvoiceTotal = viewModel.InvoiceTotal,
            TermsId = viewModel.TermsId,
            InvoiceDueDate = viewModel.InvoiceDueDate
        };
    }
}
