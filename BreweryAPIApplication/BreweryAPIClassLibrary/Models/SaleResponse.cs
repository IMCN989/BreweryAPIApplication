namespace BreweryAPIClassLibrary.Models;

public class SaleResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public decimal TotalPrice { get; set; }
}
