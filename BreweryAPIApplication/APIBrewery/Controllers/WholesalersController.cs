using BreweryAPIClassLibrary.DataAccess;
using BreweryAPIClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly IWholesalerData _wholesalerData;

        public WholesalersController(IWholesalerData wholesalerData)
        {
            _wholesalerData = wholesalerData;
        }

        // GET: api/wholesalers
        [HttpGet]
        public async Task<ActionResult<List<Wholesaler>>> GetAllWholesalers()
        {
            var wholesalers = await _wholesalerData.GetAllWholesalers();
            return Ok(wholesalers);
        }

        [HttpGet("/wholesalers/{id}")]
        public async Task<ActionResult<Wholesaler>> GetWholesalerById(int id)
        {
            var wholesaler = await _wholesalerData.GetWholesalerById(id);
            if (wholesaler == null)
            {
                return NotFound();
            }
            return Ok(wholesaler);
        }

        [HttpGet("/wholesalers/{wholesalerId}/stock/{beerId}")]
        public async Task<ActionResult<Wholesaler>> GetStockByWholesalerById(int wholesalerId, int beerId)
        {
            var wholesaler = await _wholesalerData.GetStockByWholesalerById(wholesalerId,  beerId);
            if (wholesaler == null)
            {
                return NotFound();
            }
            return Ok(wholesaler);
        }

        [HttpPost("{wholesalerId}/add-beer")]
        public async Task<IActionResult> AddBeerToWholesaler(int wholesalerId, [FromBody] AddBeerRequest request)
        {
            await _wholesalerData.AddBeerToWholesaler(wholesalerId, request.BeerId, request.Stock);
            return Ok(new { message = "Beer added to wholesaler successfully" });
        }

        public class AddBeerRequest
        {
            public int BeerId { get; set; }
            public int Stock { get; set; }
        }

        [HttpPost("quote")]
        public async Task<ActionResult<QuoteResponse>> GetQuote([FromBody] QuoteRequest request)
        {
            try
            {
                var quote = await _wholesalerData.GetQuote(request);
                return Ok(quote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellBeer([FromBody] SaleRequest request)
        {
            var response = await _wholesalerData.ProcessSale(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //// GET: api/wholesalers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Wholesaler>> GetWholesalerById(int id)
        //{
        //    var wholesaler = await _wholesalerData.GetWholesalerById(id);
        //    if (wholesaler == null)
        //    {
        //        return NotFound(new { message = "Wholesaler not found" });
        //    }
        //    return Ok(wholesaler);
        //}

        // POST: api/wholesalers
        //[HttpPost]
        //public async Task<ActionResult> AddWholesaler(Wholesaler wholesaler)
        //{
        //    await _wholesalerData.InsertWholesaler(wholesaler);
        //    return CreatedAtAction(nameof(GetWholesalerById), new { id = wholesaler.Id }, wholesaler);
        //}

        //// PUT: api/wholesalers/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateWholesaler(int id, Wholesaler wholesaler)
        //{
        //    if (id != wholesaler.Id)
        //    {
        //        return BadRequest(new { message = "ID mismatch" });
        //    }

        //    await _wholesalerData.UpdateWholesaler(wholesaler);
        //    return Ok(new { message = "Wholesaler updated successfully" });
        //}

        //// DELETE: api/wholesalers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWholesaler(int id)
        //{
        //    await _wholesalerData.DeleteWholesaler(id);
        //    return Ok(new { message = "Wholesaler deleted successfully" });
        //}
    }
}

