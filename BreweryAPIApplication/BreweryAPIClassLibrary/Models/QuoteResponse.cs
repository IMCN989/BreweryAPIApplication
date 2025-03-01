namespace BreweryAPIClassLibrary.Models;

public class QuoteResponse
{
    public decimal TotalPrice { get; set; }
    public List<QuoteSummary> Summary { get; set; } = new();
}

public class QuoteSummary
{
    public string BeerName { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
}
