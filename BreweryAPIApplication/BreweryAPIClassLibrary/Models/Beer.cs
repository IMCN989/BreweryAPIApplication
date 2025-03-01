namespace BreweryAPIClassLibrary.Models;

public class Beer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int BrewerId { get; set; }

    public static implicit operator Beer(List<Beer> v)
    {
        throw new NotImplementedException();
    }
}
