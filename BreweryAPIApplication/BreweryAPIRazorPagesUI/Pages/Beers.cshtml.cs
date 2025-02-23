using BreweryAPIClassLibrary.DataAccess;
using BreweryAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class BeersModel : PageModel
{
    private readonly IBrewerData _brewerData;

    public List<Beer> Beers { get; set; } = new();

    public BeersModel(IBrewerData brewerData)
    {
        _brewerData = brewerData;
    }

    public async Task OnGet()
    {
        Beers = await _brewerData.GetAllBeers();
    }
}
