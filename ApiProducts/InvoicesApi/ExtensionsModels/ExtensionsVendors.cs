namespace InvoicesApi;

public static class ExtensionsVendors
{
    public static VendorViewModel ToViewModel(this VendorModel model)
    {
        return new VendorViewModel
        {
            VendorName = model.VendorName,
            VendorCity = model.VendorCity,
            VendorState = model.VendorState,
            VendorZipCode = model.VendorZipCode,
            DefaultTermsId = model.DefaultTermsId,
            DefaultAccountNo = model.DefaultAccountNo
        };
    }

    public static VendorModel ToModel(this VendorViewModel viewModel)
    {
        return new VendorModel
        {
            VendorName = viewModel.VendorName,
            VendorCity = viewModel.VendorCity,
            VendorState = viewModel.VendorState,
            VendorZipCode = viewModel.VendorZipCode,
            DefaultTermsId = viewModel.DefaultTermsId,
            DefaultAccountNo = viewModel.DefaultAccountNo
        };
    }
}
