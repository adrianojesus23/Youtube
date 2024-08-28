using SqlKata.Execution;

namespace InvoicesApi;

public class InvoiceRepository: IInvoiceRepository
{
    private readonly QueryFactory _queryFactory;
    
    public InvoiceRepository(QueryFactory queryFactory)
    {
        _queryFactory = queryFactory;
    }
    
    public async Task<IEnumerable<InvoiceModel>> Get(string code, string description)
    {
        var invoices = await _queryFactory.Query("Invoices").GetAsync<InvoiceModel>();


        return invoices;
    }

    public async Task<InvoiceModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(InvoiceModel invoiceModel)
    {
        throw new NotImplementedException();
    }

    public async Task Update(InvoiceModel invoiceModel, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
