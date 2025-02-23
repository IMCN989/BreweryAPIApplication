using BreweryAPIClassLibrary.DataAccess;
using BreweryAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


public class BrewersModel : PageModel
{
    private readonly IBrewerData _brewerData;

    public List<Brewer> Brewers { get; set; } = new();

    public BrewersModel(IBrewerData brewerData)
    {
        _brewerData = brewerData;
    }

    public async Task OnGet()
    {
        Brewers = await _brewerData.GetAllBrewers();
    }
}
