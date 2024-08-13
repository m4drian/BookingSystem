using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DesksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetDesks()
    {
        return Ok(Array.Empty<string>());
    }
}