using BreweryAPIClassLibrary.Models;

namespace BreweryAPIClassLibrary.DataAccess
{
    public interface IWholesalerData
    {
        Task AddBeerToWholesaler(int wholesalerId, int beerId, int stock);
        Task AddSale(int wholesalerId, int beerId, int quantity);
        Task<List<Wholesaler>> GetAllWholesalers();
        Task<List<Beer>> GetBeersByBrewer(int brewerId);
        Task<QuoteResponse> GetQuote(QuoteRequest request);
        Task<List<WholesalerBeer>> GetStockByWholesalerById(int wholesalerId, int beerId);
        Task<Wholesaler> GetWholesalerById(int wholesalerId);
        Task<SaleResponse> ProcessSale(SaleRequest request);
    }
}