using BreweryAPIClassLibrary.Models;
using System.Data;


namespace BreweryAPIClassLibrary.DataAccess
{
    public class BrewerData : IBrewerData
    {
        private readonly ISqlDataAccess _db;

        public BrewerData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<Beer>> GetAllBeers()
        {
            return await _db.LoadData<Beer, dynamic>("spBeers_GetAll", new { }, "Default");
        }

        public async Task<List<Brewer>> GetAllBrewers()
        {
            return await _db.LoadData<Brewer, dynamic>("spBrewers_GetAll", new { }, "Default");
        }
        public async Task<List<Beer>> GetBeersByBrewer(int brewerId)
        {
            return await _db.LoadData<Beer, dynamic>("spBeers_GetByBrewer", new { BrewerId = brewerId }, "Default");
        }

        public async Task<Beer> GetBeerById(int beerId)
        {
            return await _db.GetDataById<Beer>("spBeers_GetBeerById", new { BeerId = beerId }, "Default");
        }

        public async Task AddBeer(Beer beer)
        {
            await _db.SaveData("spBeers_Insert", new { beer.Name, beer.Price, beer.BrewerId }, "Default");

        }

        public async Task DeleteBeer(int beerId)
        {
            await _db.SaveData("spBeers_Delete", new { Id = beerId }, "Default");
        }

        public async Task UpdateBeer(Beer beer)
        {
            await _db.SaveData("spBeers_Update", beer, "Default");
        }
    }
}
