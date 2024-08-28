using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;

namespace DummyJsonApi.WebApi;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly HttpClient? _httpClient;
    private readonly string _urlProduct = "https://dummyjson.com/products";
    private readonly string _urlProductName = "ProductCache";
    public ProductService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
    {
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _httpClient = _httpClientFactory.CreateClient("DummyJson");
    }

    public Task Create(ProductModel productModel)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductModel>> Get(ProductFilter productFilter)
    {
        if (_httpClient == null)
            return Enumerable.Empty<ProductModel>();

        var response = await _httpClient.GetAsync(_urlProduct);

        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<ProductModel>();

        var responseData = await response.Content.ReadFromJsonAsync<ProductResponse>();

        if (responseData == null || !responseData.Products.Any())
            return Enumerable.Empty<ProductModel>();

        var products = responseData.Products;

        // Apply the filters directly
        return products;
    }
    /*
    Resumo das Diferenças
IOptions: Acesso simples e estático a configurações; instância única durante o ciclo de vida da aplicação.
IOptionsMonitor: Acesso dinâmico e notificação de alterações de configurações; permite reagir a mudanças em tempo de execução.
IOptionsSnapshot: Acesso a uma "foto instantânea" das configurações que são recarregadas por requisição em um contexto web.
IOptionsFactory: Fábrica interna usada pelo sistema de Options para criar instâncias de configurações; raramente usada diretamente
    */

    public async Task<IEnumerable<ProductModel>> GetByFilter(ProductFilter productFilter)
    {
          if (_httpClient == null)
            return Enumerable.Empty<ProductModel>();

        IQueryable<ProductModel>?  productsQuery =_memoryCache.Get<IQueryable<ProductModel>>(_urlProductName);

        if ( productsQuery == null)
        {
             productsQuery = await FetchProductsAsync();
             productsQuery = ApplySpecificationFilter(productFilter, productsQuery);
        }else
             productsQuery = ApplySpecificationFilter(productFilter, productsQuery);

        if (productsQuery is null || !productsQuery.Any())
        {
            productsQuery = await FetchProductsAsync();
            productsQuery = ApplySpecificationFilter(productFilter, productsQuery);
        }

        
        _memoryCache.Set(_urlProductName, productsQuery, TimeSpan.FromMinutes(5));

        return productsQuery.ToList();
    }

    private static IQueryable<ProductModel> ApplySpecificationFilter(ProductFilter productFilter, IQueryable<ProductModel> productsQuery)
    {
        
         var spec = new ProductSpecification(productFilter);
 
        // Aplicando o filtro
        if (spec.Filter != null)
            productsQuery = productsQuery.Where(spec.Filter);

        // Aplicando a ordenação
        if (spec.OrderBy != null)
            productsQuery = productsQuery.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending != null)
            productsQuery = productsQuery.OrderByDescending(spec.OrderByDescending);

        // Aplicando o agrupamento
        if (spec.GroupBy != null)
            productsQuery = productsQuery.GroupBy(spec.GroupBy).SelectMany(g => g);
        return productsQuery;
    }

    private async Task<IQueryable<ProductModel>> FetchProductsAsync()
    {
        var response = await _httpClient.GetAsync(_urlProduct);
        if (!response.IsSuccessStatusCode)
            return (IQueryable<ProductModel>)Enumerable.Empty<ProductModel>();

        var responseData = await response.Content.ReadFromJsonAsync<ProductResponse>();
        if (responseData == null || !responseData.Products.Any())
            return (IQueryable<ProductModel>)Enumerable.Empty<ProductModel>();

        return responseData.Products.AsQueryable();
    }

    // Define a class to match the response structure
    public class ProductResponse
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }

    public async Task<ProductModel> GetById(int id)
    {
        if (id <= 0 || _httpClient is null)
            return await Task.FromResult(new ProductModel());

        var client = await _httpClient.GetFromJsonAsync<ProductModel>($"{_urlProduct}/{id}");

        return client ?? new ProductModel();
    }

    public Task Update(ProductModel productModel)
    {
        throw new NotImplementedException();
    }
}

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Filter { get; }

    Expression<Func<T, object>> OrderBy { get; }

    Expression<Func<T, object>> OrderByDescending { get; }

    Expression<Func<T, object>> GroupBy { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    Expression<Func<T, bool>> Contains(Expression<Func<T, object>> selector, string value);

    bool IsSatisfiedBy(T entity);
}

public abstract class SpecificationBase<T> : ISpecification<T> where T : class
{
    public virtual Expression<Func<T, bool>> Filter { get; protected set; } = x => true;
    public virtual Expression<Func<T, object>> OrderBy { get; protected set; }
    public virtual Expression<Func<T, object>> OrderByDescending { get; protected set; }
    public virtual Expression<Func<T, object>> GroupBy { get; protected set; }
    public virtual List<Expression<Func<T, object>>> Includes { get; protected set; } = new();

    public virtual Expression<Func<T, bool>> Contains(Expression<Func<T, object>> selector, string value)
    {
        var parameter = selector.Parameters.First();
        var toStringCall = Expression.Call(
            Expression.Convert(selector.Body, typeof(string)),
            typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) }),
            Expression.Constant(value),
            Expression.Constant(StringComparison.OrdinalIgnoreCase)
        );

        return Expression.Lambda<Func<T, bool>>(toStringCall, parameter);
    }

    public virtual bool IsSatisfiedBy(T entity)
    {
        return Filter.Compile().Invoke(entity);
    }
}

public class ProductSpecification : SpecificationBase<ProductModel>
{
    public ProductSpecification(ProductFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.description))
            Filter = product => product.description.Contains(filter.description, StringComparison.OrdinalIgnoreCase);

        if (!string.IsNullOrEmpty(filter.title))
            Filter = Filter.And(product => product.title.Contains(filter.title, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(filter.category))
            Filter = Filter.And(product => product.category.Contains(filter.category, StringComparison.OrdinalIgnoreCase));

        if (filter.price > 0)
            Filter = Filter.And(product => product.price <= filter.price);

        if (filter.discountPercentage > 0)
            Filter = Filter.And(product => product.discountPercentage >= filter.discountPercentage);

        // Exemplos de ordenação e agrupamento
        OrderBy = product => product.price;
        OrderByDescending = product => product.discountPercentage;
        GroupBy = product => product.category;
    }
}

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var visitor1 = new ReplaceExpressionVisitor();
        visitor1.Add(expr1.Parameters[0], parameter);
        var body1 = visitor1.Visit(expr1.Body);

        var visitor2 = new ReplaceExpressionVisitor();
        visitor2.Add(expr2.Parameters[0], parameter);
        var body2 = visitor2.Visit(expr2.Body);

        var combined = Expression.AndAlso(body1, body2);

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

}

public class ReplaceExpressionVisitor : ExpressionVisitor
{
    private readonly Dictionary<Expression, Expression> _replacements;

    public ReplaceExpressionVisitor()
    {
        _replacements = new Dictionary<Expression, Expression>();
    }

    public void Add(Expression from, Expression to)
    {
        _replacements[from] = to;
    }

    public override Expression Visit(Expression node)
    {
        if (node != null && _replacements.TryGetValue(node, out var replacement))
        {
            return replacement;
        }

        var delegateMethod = new DelegateMethod();
        System.Console.WriteLine(delegateMethod.CountPublic(3));

        return base.Visit(node);
    }
}

delegate int Transformer(int x);
public class DelegateMethod
{
    private int Count(int x)
    {
        return x * 2;
    }


    public int CountPublic(int x)
    {
        Transformer transformer = Count;

        Transformer transformer1 = new Transformer(Count);

        var result = transformer1.Invoke(2);

        return transformer(x);
    }


    delegate VehicleModel SetVehicleDelegate(int id, string description);
    delegate T GetVehicle<T>(T t) where T : class;

    public record VehicleModel(int id, string description);

    public class Vehicle
    {
        private static VehicleModel SetVehicle(int id, string description)
        {
            return new VehicleModel(id, description);
        }

        public static VehicleModel Create(int id, string description)
        {
            SetVehicleDelegate vehicle = new SetVehicleDelegate(SetVehicle);

            return vehicle.Invoke(id, description);
        }
    }

    public static class VehicleImplement
    {
        public static VehicleModel GetVehicle()
        {
            var result = Vehicle.Create(100, "Ibiza");

            return result;
        }


        public static Action<VehicleModel> SetVehicle = vehicle => System.Console.WriteLine(vehicle.description);

        public static Predicate<VehicleModel> PredicateFilter = vehicle => vehicle.description.Contains('n');

        public static void ThreadStart()
        {

            IEnumerable<VehicleModel> list = new List<VehicleModel>();
            var vehicles = new List<VehicleModel>
        {
            new VehicleModel(1,"A3"),
            new VehicleModel(2,"A3"),
           new VehicleModel(4,"A3"),
        };

            // Usando o método de extensão Where3 com o Func definido
            var filteredVehicles = vehicles.Where3(VehicleExtensions.func, "a");
        }
    }



}
