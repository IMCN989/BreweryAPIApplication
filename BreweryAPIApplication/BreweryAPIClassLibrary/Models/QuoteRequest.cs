namespace BreweryAPIClassLibrary.Models;

public class QuoteRequest
{
    public int WholesalerId { get; set; }
    public List<QuoteItem> Items { get; set; } = new();
}

public class QuoteItem
{
    public int BeerId { get; set; }
    public int Quantity { get; set; }
}
