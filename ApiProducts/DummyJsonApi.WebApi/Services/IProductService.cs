namespace DummyJsonApi.WebApi;

public interface IProductService
{
   Task<IEnumerable<ProductModel>>Get(ProductFilter productFilter);
   Task<IEnumerable<ProductModel>> GetByFilter(ProductFilter productFilter);
   Task<ProductModel>GetById(int id);
   Task Create(ProductModel productModel);
   Task Update(ProductModel productModel);
   Task Delete(int id);

}
