using BreweryAPIClassLibrary.Models;

namespace BreweryAPIClassLibrary.DataAccess
{
    public interface IBrewerData
    {
        Task AddBeer(Beer beer);
        Task DeleteBeer(int beerId);
        Task<List<Beer>> GetAllBeers();
        Task<List<Brewer>> GetAllBrewers();
        Task<Beer> GetBeerById(int beerId);
        Task<List<Beer>> GetBeersByBrewer(int brewerId);
        Task UpdateBeer(Beer beer);
    }
}