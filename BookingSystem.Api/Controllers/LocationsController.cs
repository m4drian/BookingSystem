using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly ISender _mediator;

    public LocationsController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public IActionResult CreateLocation()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpPut("{locationName}")]
    public IActionResult UpdateLocation()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("all")]
    public IActionResult GetLocations()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("desks/{locationName}")]
    public IActionResult GetDesksFromLocation()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpDelete("{locationName}")]
    public IActionResult DeleteLocation()
    {
        return Ok(Array.Empty<string>());
    }
}