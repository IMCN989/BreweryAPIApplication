using BreweryAPIClassLibrary.Models;

namespace BreweryAPIClassLibrary.DataAccess
{
    public interface IWholesalerData
    {
        Task AddSale(int wholesalerId, int beerId, int quantity);
        Task<List<Wholesaler>> GetAllWholesalers();
        Task<decimal?> GetQuote(int wholesalerId, Dictionary<int, int> order);
    }
}