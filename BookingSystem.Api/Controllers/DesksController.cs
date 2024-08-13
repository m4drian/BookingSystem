using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DesksController : ControllerBase
{
    private readonly ISender _mediator;

    public DesksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{locationName}")]
    public IActionResult CreateDesk()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpPut("reservation/{deskId}")]
    public IActionResult UpdateDeskEmployee()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpPut("reservation/admin/{deskId}")]
    public IActionResult UpdateDeskAdmin()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("all")]
    public IActionResult GetDesks()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("{deskId}")]
    public IActionResult GetDeskReservation()
    {
        return Ok(Array.Empty<string>());
    }

    [HttpDelete("{deskId}")]
    public IActionResult DeleteDesk()
    {
        return Ok(Array.Empty<string>());
    }
}