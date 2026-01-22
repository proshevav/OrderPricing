public class PricingResponse
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Country { get; set; } = null!;
    public decimal Subtotal { get; set; }

    public required DiscountInfo Discount { get; set; }
    public decimal SubtotalAfterDiscount { get; set; }

    public required TaxInfo Tax { get; set; }
    public decimal FinalPrice { get; set; }
}


public class DiscountInfo
{
    public decimal Percentage { get; set; }
    public decimal Amount { get; set; }
}

public class TaxInfo
{
    public string Country { get; set; } = null!;
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
}

