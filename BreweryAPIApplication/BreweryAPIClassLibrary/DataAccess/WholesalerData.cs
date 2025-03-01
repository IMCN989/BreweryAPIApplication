
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

        public async Task<Wholesaler> GetWholesalerById(int wholesalerId)
        {
            return await _db.GetDataById<Wholesaler>("spWholesalers_GetById", new { WholesalerId = wholesalerId }, "Default");
        }

        public async Task<List<WholesalerBeer>> GetStockByWholesalerById(int wholesalerId, int beerId)
        {
            //Validate wholesaler exists
            var wholesaler = await _db.GetDataById<Wholesaler>("spWholesalers_GetById", new { WholesalerId = wholesalerId }, "Default");


            if (wholesaler == null)
                throw new Exception("Wholesaler does not exist.");

            //Get wholesaler's stock
            return await _db.LoadData<WholesalerBeer, dynamic>(
                "spWholesaler_GetStock", new { WholesalerId = wholesalerId, BeerId = beerId }, "Default");
        }


        public async Task<List<Beer>> GetBeersByBrewer(int brewerId)
        {
            return await _db.LoadData<Beer, dynamic>("spBeers_GetByBrewer", new { BrewerId = brewerId }, "Default");
        }

        public async Task AddBeerToWholesaler(int wholesalerId, int beerId, int stock)
        {
            await _db.SaveData("spWholesaler_AddBeer", new { WholesalerId = wholesalerId, BeerId = beerId, Stock = stock }, "Default");
        }


        public async Task AddSale(int wholesalerId, int beerId, int quantity)
        {
            await _db.SaveData("spWholesaler_AddSale", new { WholesalerId = wholesalerId, BeerId = beerId, Quantity = quantity }, "Default");
        }



        public async Task<QuoteResponse> GetQuote(QuoteRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new Exception("Order cannot be empty.");

            //Validate wholesaler exists
            var wholesaler = await _db.GetDataById<Wholesaler>("spWholesalers_GetById", new { WholesalerId = request.WholesalerId }, "Default");


            if (wholesaler == null)
                throw new Exception("Wholesaler does not exist.");

            //Get wholesaler's stock
            var stock = await _db.LoadData<WholesalerBeer, dynamic>(
                "spWholesaler_GetStock", new { WholesalerId = request.WholesalerId }, "Default");

            var response = new QuoteResponse();

            foreach (var item in request.Items)
            {
                var beerStock = stock.FirstOrDefault(s => s.BeerId == item.BeerId);

                if (beerStock == null)
                    throw new Exception($"Beer ID {item.BeerId} is not sold by this wholesaler.");

                if (beerStock.Stock < item.Quantity)
                    throw new Exception($"Not enough stock for Beer ID {item.BeerId}.");

                decimal totalPrice = beerStock.BeerPrice * item.Quantity;
                decimal discount = 0;

                if (item.Quantity > 20)
                    discount = 20;
                else if (item.Quantity > 10)
                    discount = 10;

                decimal finalPrice = totalPrice * (1 - discount / 100);

                response.Summary.Add(new QuoteSummary
                {
                    BeerName = beerStock.BeerName,
                    Quantity = item.Quantity,
                    PricePerUnit = beerStock.BeerPrice,
                    TotalPrice = totalPrice,
                    Discount = discount,
                    FinalPrice = finalPrice
                });
            }

            response.TotalPrice = response.Summary.Sum(x => x.FinalPrice);
            return response;
        }

        public async Task<SaleResponse> ProcessSale(SaleRequest request)
        {
            // 1. Get Beer Price & Available Stock
            var beerStock = await _db.LoadData<WholesalerBeer, dynamic>(
                "spWholesaler_GetStock",
                new { request.WholesalerId, request.BeerId }, "Default");

            if (beerStock == null || !beerStock.Any())
            {
                return new SaleResponse { Success = false, Message = "Beer not found for this wholesaler." };
            }

            var stockData = beerStock.First();
            if (stockData.Stock < request.Quantity)
            {
                return new SaleResponse { Success = false, Message = "Not enough stock available." };
            }

            // 2. Calculate Total Price & Apply Discount if quantity >= 10
            decimal totalPrice = stockData.BeerPrice * request.Quantity;
            if (request.Quantity >= 10)
            {
                totalPrice *= 0.90m; // 10% discount
            }

            // 3. Update Stock in Database
            await _db.SaveData(
                "spWholesalers_UpdateStock",
                new { request.WholesalerId, request.BeerId, request.Quantity }, "Default");

            // 4. Insert Sale Record
            await _db.SaveData(
                "spSales_Insert",
                new { request.WholesalerId, request.BeerId, request.Quantity, TotalPrice = totalPrice }, "Default");

            return new SaleResponse
            {
                Success = true,
                Message = "Sale completed successfully.",
                TotalPrice = totalPrice
            };
        }




    }
}
