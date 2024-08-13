using BookingSystem.Contracts.Desks;
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
    public IActionResult CreateDesk(
        CreateDeskRequest request, 
        string locationName)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("all")]
    public IActionResult GetDesks(GetDesksRequest request)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("{deskId}")]
    public IActionResult GetDeskReservation(
        GetDeskReservationRequest request, 
        string deskId)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpPut("reservation/{deskId}")]
    public IActionResult UpdateDeskEmployee(
        UpdateDeskEmployeeRequest request, 
        string deskId)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpPut("reservation/admin/{deskId}")]
    public IActionResult UpdateDeskAdmin(
        UpdateDeskAdminRequest request, 
        string deskId)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpDelete("{deskId}")]
    public IActionResult DeleteDesk(
        DeleteDeskRequest request, 
        string deskId)
    {
        return Ok(Array.Empty<string>());
    }
}