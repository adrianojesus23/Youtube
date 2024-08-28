using InvoicesApi.Infrastructures;
using OneOf;

namespace InvoicesApi;

public class InvoiceService:IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
       // _logger = logger;
    }
    public async Task<OneOf<IEnumerable<InvoiceViewModel>, AppError>> Get(string code, string description)
    {
        //_logger.LogInformation("Method Get Invoice called with code: {Code} and description: {Description}", code, description);

        var invoices = await _invoiceRepository.Get(code, description);
        
        if (invoices != null || !invoices.Any())
            return  new ShouldNotFound();

        return invoices.Select(invoice => invoice.ToViewModel()).ToList();
    }

}
