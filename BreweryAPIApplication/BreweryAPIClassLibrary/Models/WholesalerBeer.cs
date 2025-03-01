
namespace BreweryAPIClassLibrary.Models;

public class WholesalerBeer
{
    public int WholesalerId { get; set; }
    public int BeerId { get; set; }
    public int Stock { get; set; }
    public string BeerName { get; set; }
    public decimal BeerPrice { get; set; }

    public static implicit operator WholesalerBeer(List<WholesalerBeer> v)
    {
        throw new NotImplementedException();
    }
}


