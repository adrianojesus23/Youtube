namespace InvoicesApi;

public interface IInvoiceRepository
{
    Task<IEnumerable<InvoiceModel>> Get(string code, string description);
    Task<InvoiceModel> GetById(int id);
    Task<int> Create(InvoiceModel invoiceModel);
    Task Update(InvoiceModel invoiceModel, int id);
    Task<bool> Delete(int id);
}
