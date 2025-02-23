using BreweryAPIClassLibrary.DataAccess;

using BreweryAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreweryAPIRazorPagesUI.Pages;

public class DeleteModel : PageModel
{
    private readonly IBrewerData _beerData;

    public DeleteModel(IBrewerData beerData)
    {
        _beerData = beerData;
    }

    [BindProperty]
    public Beer Beer { get; set; } = new Beer(); // Ensure Beer is initialized

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Beer = await _beerData.GetBeerById(id);
        if (Beer == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        await _beerData.DeleteBeer(id);
        return RedirectToPage("./Index");
    }
}
