using Microsoft.AspNetCore.Mvc;
using BreweryAPIClassLibrary.Models;
using BreweryAPIClassLibrary.DataAccess;


namespace BreweryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly IBrewerData _brewerData;

        public BrewersController(IBrewerData brewerData)
        {
            _brewerData = brewerData;
        }

        [HttpGet("/beers")]
        public async Task<ActionResult<List<Beer>>> GetAllBeers()
        {
            var beers = await _brewerData.GetAllBeers();
            return Ok(beers);
        }

        [HttpGet("/brewers")]
        public async Task<ActionResult<List<Brewer>>> GetAllBrewers()
        {
            var brewers = await _brewerData.GetAllBrewers();
            return Ok(brewers);
        }

        [HttpGet("{brewerId}/beers")]
        public async Task<ActionResult<List<Beer>>> GetBeersByBrewer(int brewerId)
        {
            var beers = await _brewerData.GetBeersByBrewer(brewerId);
            return Ok(beers);
        }

        [HttpPost("addBeer")]
        public async Task<IActionResult> AddBeer(Beer beer)
        {
            await _brewerData.AddBeer(beer);
            return Ok("Beer added successfully");
        }

        [HttpDelete("deleteBeer/{beerId}")]
        public async Task<IActionResult> DeleteBeer(int beerId)
        {
            await _brewerData.DeleteBeer(beerId);
            return Ok("Beer deleted successfully");
        }

        [HttpPut("updateBeer")]
        public async Task<IActionResult> UpdateBeer(Beer beer)
        {
            await _brewerData.UpdateBeer(beer);
            return Ok("Beer updated successfully");
        }
    }
}
