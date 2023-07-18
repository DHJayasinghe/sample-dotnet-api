using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient();

var app = builder.Build();

app.MapGet("/products", async (IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    HttpResponseMessage response = await client.GetAsync("https://dummyjson.com/products");
    if (response.IsSuccessStatusCode)
    {
        string jsonResponse = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<ProductResult>(jsonResponse);
        return products;
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
        return null;
    }
});

app.Run();
public record ProductResult
{
    public List<Product> Products { get; set; }
}
public record Product
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public decimal DiscountPercentage { get; init; }
    public decimal Rating { get; init; }
    public int Stock { get; init; }
    public string Brand { get; init; }
    public string Category { get; init; }
    public string Thumbnail { get; init; }
    public List<string> Images { get; init; }
}