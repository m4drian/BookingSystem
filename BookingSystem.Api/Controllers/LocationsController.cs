using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetLocations()
    {
        return Ok(Array.Empty<string>());
    }
}