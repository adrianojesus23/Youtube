using InvoicesApi.Infrastructures;
using OneOf;

namespace InvoicesApi;

public interface IInvoiceService
{
  Task<OneOf<IEnumerable<InvoiceViewModel>, AppError>> Get(string code, string description);
}
