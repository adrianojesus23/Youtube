using DummyJsonApi.WebApi;
using DummyJsonApi.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneOf;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService,
    IConfiguration configuration,
    IOptions<CountryModel> options,
    IOptionsFactory<CountryModel> optionsFactory,
    IOptionsMonitor<CountryModel> optionsMonitor,
    IOptionsSnapshot<CountryModel> optionsSnapshot) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IOptions<CountryModel> _options = options;
        private readonly IOptionsFactory<CountryModel> _optionsFactory = optionsFactory;
        private readonly IOptionsMonitor<CountryModel> _optionsMonitor = optionsMonitor;
        private readonly IOptionsSnapshot<CountryModel> _optionsSnapshot = optionsSnapshot;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductFilter productFilter)
        {
            var products = await _productService.Get(productFilter);

            if (products == null || !products.Any())
            {
                return NotFound();  // Return 404 if no products found
            }

            return Ok(products);  // Return 200 with the product list
        }
        [HttpGet("filtor")]
        public async Task<IActionResult> GetFiltor([FromQuery] ProductFilter productFilter)
        {
            var products = await _productService.GetByFilter(productFilter);

            if (products == null || !products.Any())
            {
                return NotFound();  // Return 404 if no products found
            }

            return Ok(products);  // Return 200 with the product list
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();  // Return 404 if no products found
            }

            return Ok(product);  // Return 200 with the product list
        }
        [HttpGet("Configurations")]
        public async Task<IActionResult> GetConfigurations()
        {
            var currency = _configuration.GetConfig<string>("Country:Currency");
            var discount = _configuration.GetConfig<int>("Country:Discount");
            var reviews = _configuration.GetConfig<bool>("Country:Reviews");

            var resut = new CountryModel { Currency = currency ?? "", Discount = discount, Reviews = reviews };

            var resultTask = await Task.FromResult(resut);

            return Ok(resultTask);
        }

        [HttpGet("ConfigurationSections")]
        public async Task<IActionResult> GetConfigurationSections()
        {
            var country = _configuration.GetSection("Country")
                                        .Get<CountryModel>();

            var resultTask = await Task.FromResult(country);

            return Ok(resultTask);
        }
[HttpGet("ConfigurationBinds")]
public async Task<IResult> GetConfigurationBinds()
        {
          var result = NewMethod();

          if(result.IsError())
                return Results.NotFound(result.AsT1);

         if(result.IsSuccess())
            return Results.Ok(result.AsT0);

            return Results.NoContent();
        }

        private OneOf<CountryModel, AppError> NewMethod()
        {
             CountryModel countryModel = new CountryModel();
            _configuration.GetSection("Country").Bind(countryModel);

            // Verifica se o binding foi feito com sucesso (usando uma propriedade obrigatória do modelo como critério)
            if (string.IsNullOrEmpty(countryModel?.Currency)) // Supondo que 'Name' seja uma propriedade obrigatória
            {
                return new ShouldNotFound();
            }

            return countryModel;
        }

        [HttpGet("ConfigurationOptions")]
        public async Task<IActionResult> GetConfigurationOptions()
        {
            return Ok(_options);
        }
        [HttpGet("ConfigurationOptionsM")]
        public async Task<IActionResult> GetConfigurationOptionsM()
        {
            System.Console.WriteLine(_optionsMonitor.OnChange(op =>
            {
                _optionsMonitor.CurrentValue.Currency = "Dollar";
            }));
            return Ok(_optionsMonitor);
        }
        [HttpGet("ConfigurationOptionsS")]
        public async Task<IActionResult> GetConfigurationOptionsS()
        {
            return Ok(_optionsSnapshot);
        }

        [HttpGet("ConfigurationOptionsF")]
        public async Task<IActionResult> GetConfigurationOptionsF()
        {
            return Ok(_optionsFactory);
        }
    }
}


public static class ConfigurationSetting
{


    public static T? GetConfig<T>(this IConfiguration config, string key)
    {
        return config.GetValue<T>(key);
    }
}