using BreweryAPIClassLibrary.DataAccess;
using BreweryAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class WholesalersModel : PageModel
{
    private readonly IWholesalerData _wholesalerData;

    public List<Wholesaler> Wholesalers { get; set; } = new();

    public WholesalersModel(IWholesalerData wholesalerData)
    {
        _wholesalerData = wholesalerData;
    }

    public async Task OnGet()
    {
        Wholesalers = await _wholesalerData.GetAllWholesalers();
    }
}
