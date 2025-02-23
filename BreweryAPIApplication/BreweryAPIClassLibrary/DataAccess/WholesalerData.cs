
using BreweryAPIClassLibrary.Models;
using Newtonsoft.Json;

namespace BreweryAPIClassLibrary.DataAccess
{
    public class WholesalerData : IWholesalerData
    {
        private readonly ISqlDataAccess _db;

        public WholesalerData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<Wholesaler>> GetAllWholesalers()
        {
            return await _db.LoadData<Wholesaler, dynamic>("spWholesalers_GetAll", new { }, "Default");
        }


        public async Task AddSale(int wholesalerId, int beerId, int quantity)
        {
            await _db.SaveData("spWholesaler_AddSale", new { WholesalerId = wholesalerId, BeerId = beerId, Quantity = quantity }, "Default");
        }

        public async Task<decimal?> GetQuote(int wholesalerId, Dictionary<int, int> order)
        {
            var total = await _db.LoadData<decimal, dynamic>("spWholesaler_GetQuote", new { WholesalerId = wholesalerId, OrderJson = JsonConvert.SerializeObject(order) }, "Default");
            return total.Count > 0 ? total[0] : null;
        }
    }
}
