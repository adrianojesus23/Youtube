namespace DummyJsonApi.WebApi;

public class ProductModel
{
    public int id { get; init; }
    public string title { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public float price { get; set; }
    public float discountPercentage { get; set; }
    public float rating { get; set; }
    public int stock { get; set; }
    public string[] tags { get; set; }
    public string brand { get; set; }
    public string sku { get; set; }
    public int weight { get; set; }
    public Dimensions dimensions { get; set; }
    public string warrantyInformation { get; set; }
    public string shippingInformation { get; set; }
    public string availabilityStatus { get; set; }
    public Review[] reviews { get; set; }
    public string returnPolicy { get; set; }
    public int minimumOrderQuantity { get; set; }
    public Meta meta { get; set; }
    public string[] images { get; set; }
    public string thumbnail { get; set; }
    public IList<Brand> Brands{ get; set; } = new List<Brand>();
}

public class Brand
{
    public int Id { get; set; }
    public string name { get; set;}
    public string description { get; set;}
}

public class Dimensions
{
    public float width { get; set; }
    public float height { get; set; }
    public float depth { get; set; }
}

public class Meta
{
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string barcode { get; set; }
    public string qrCode { get; set; }
}

public class Review
{
    public int rating { get; set; }
    public string comment { get; set; }
    public DateTime date { get; set; }
    public string reviewerName { get; set; }
    public string reviewerEmail { get; set; }
}

public record  ProductFilter 
{
    public string title { get; set; } = string.Empty;
    public string description { get; set; }= string.Empty;
    public string category { get; set; }= string.Empty;
    public float price { get; set; }
    public float discountPercentage { get; set; }
}