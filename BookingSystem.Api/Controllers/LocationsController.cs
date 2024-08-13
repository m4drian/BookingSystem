using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocationsController : ControllerBase
{
    [HttpGet("list")]
    public IActionResult ListLocations()
    {
        return Ok(Array.Empty<string>());
    }
}