using Microsoft.AspNetCore.Mvc;
using BreweryClassLibrary.DataAccess;
using BreweryClassLibrary.Models;
using System.Security.Claims;

namespace BreweryAPI.Controllers;



[Route("api/[controller]")]
[ApiController]
public class BreweryController : ControllerBase
{
    private readonly IBeerData _data;

    public BreweryController(IBeerData data)
    {
        _data = data;
    }

    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdText);
    }

    // GET: api/Beers
    [HttpGet]
    public async Task<ActionResult<List<BreweryModel>>> Get()
    {
        var output = await _data.GetAllAssigned(GetUserId());

        return Ok(output);
    }

    // GET api/Beers/5
    [HttpGet("{beerId}")]
    public async Task<ActionResult<BreweryModel>> Get(int beerId)
    {
        var output = await _data.GetOneAssigned(GetUserId(), beerId);

        return Ok(output);
    }

    // POST api/Beers
    [HttpPost]
    public async Task<ActionResult<BreweryModel>> Post([FromBody] string task)
    {
        var output = await _data.Create(GetUserId(), task);

        return Ok(output);
    }

    // PUT api/Beers/5
    [HttpPut("{beerId}")]
    public async Task<ActionResult> Put(int beerId, [FromBody] string task)
    {
        await _data.UpdateTask(GetUserId(), beerId, task);

        return Ok();
    }

    // PUT api/Beers/5/Complete
    [HttpPut("{beerId}/Complete")]
    public async Task<IActionResult> Complete(int beerId)
    {
        await _data.CompleteBeer(GetUserId(), beerId);

        return Ok();
    }

    // DELETE api/Beers/5
    [HttpDelete("{beerId}")]
    public async Task<IActionResult> Delete(int beerId)
    {
        await _data.Delete(GetUserId(), beerId);

        return Ok();
    }
}
