using BookingSystem.Contracts.Locations;
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
    public IActionResult CreateLocation(CreateLocationRequest request)
    {
        return Ok(request);
    }

    [HttpPut("{locationName}")]
    public IActionResult UpdateLocation(
        UpdateLocationRequest request, 
        string locationName)
    {
        return Ok(request);
    }

    [HttpGet("all")]
    public IActionResult GetLocations(
        GetLocationsRequest request)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpGet("desks/{locationName}")]
    public IActionResult GetDesksFromLocation(
        GetDesksFromLocationRequest request, 
        string locationName)
    {
        return Ok(Array.Empty<string>());
    }

    [HttpDelete("{locationName}")]
    public IActionResult DeleteLocation(
        DeleteLocationRequest request, 
        string locationName)
    {
        return Ok(Array.Empty<string>());
    }
}