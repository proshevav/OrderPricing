using System.Text.Json;

public class PricingService
{
    private readonly List<Product> _products;

public PricingService()
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "products.json");

    if (!File.Exists(filePath))
        throw new FileNotFoundException("Could not find products.json at runtime", filePath);

    var json = File.ReadAllText(filePath);

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    var catalog = JsonSerializer.Deserialize<ProductCatalog>(json, options)
                  ?? throw new Exception("Failed to deserialize product catalog");

    _products = catalog.Products;
}



    public PricingResponse CalculatePrice(OrderRequest request)
    {
        var product = GetProduct(request.ProductId);
        decimal subtotal = request.Quantity * product.Price;

        decimal discountPct = CalculateDiscount(request.Quantity, subtotal);
        decimal discountAmount = subtotal * discountPct;

        decimal subtotalAfterDiscount = subtotal - discountAmount;

        decimal taxRate = GetTaxRate(request.Country);
        decimal taxAmount = subtotalAfterDiscount * taxRate;

        decimal finalPrice = subtotalAfterDiscount + taxAmount;

        return BuildResponse(
            product,
            request,
            subtotal,
            discountPct,
            discountAmount,
            subtotalAfterDiscount,
            taxRate,
            taxAmount,
            finalPrice
        );
    }

    private decimal CalculateDiscount(int quantity, decimal subtotal)
    {
        if (subtotal < 500) return 0;

        if (quantity >= 100) return 0.15m;
        if (quantity >= 50) return 0.10m;
        if (quantity >= 10) return 0.05m;

        return 0;
    }

    private decimal GetTaxRate(string country) => country switch
    {
        "MK" => 0.18m,
        "DE" => 0.20m,
        "FR" => 0.20m,
        "USA" => 0.10m,
        _ => throw new ArgumentException("Unsupported country")
    };

    private Product GetProduct(string productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId)
            ?? throw new ArgumentException("Product not found");
    }

    // 👇 PUT IT HERE (inside the class)
    private PricingResponse BuildResponse(
        Product product,
        OrderRequest request,
        decimal subtotal,
        decimal discountPct,
        decimal discountAmount,
        decimal subtotalAfterDiscount,
        decimal taxRate,
        decimal taxAmount,
        decimal finalPrice)
    {
        return new PricingResponse
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Quantity = request.Quantity,
            UnitPrice = product.Price,
            Country = request.Country,
            Subtotal = subtotal,
            Discount = new DiscountInfo
            {
                Percentage = discountPct,
                Amount = discountAmount
            },
            SubtotalAfterDiscount = subtotalAfterDiscount,
            Tax = new TaxInfo
            {
                Country = request.Country,
                Rate = taxRate,
                Amount = taxAmount
            },
            FinalPrice = finalPrice
        };
    }
}
