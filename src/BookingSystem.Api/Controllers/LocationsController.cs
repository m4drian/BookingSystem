using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Desks.Common;
using BookingSystem.Application.Locations.Commands;
using BookingSystem.Application.Locations.Common;
using BookingSystem.Application.Locations.Queries;
using BookingSystem.Contracts.Common.DTO;
using BookingSystem.Contracts.Desks.Responses;
using BookingSystem.Contracts.Locations;
using BookingSystem.Contracts.Locations.Responses;
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
    public async Task<IActionResult> CreateLocation(CreateLocationRequest request)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new CreateLocationCommand(
                request.Name,
                request.Description
            );

            LocationResult locationResult = await _mediator.Send(command);

            var response = new CreateLocationResponse(
                locationResult.Location.Name
            );

            return Ok(response);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch(Exception ex)
        {
            if (ex is IServiceException se)
            {
                return StatusCode((int)se.StatusCode, se.ErrorMessage);
            }
            else
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetLocations(
        GetLocationsRequest request)
    {
        try{
            var command = new GetLocationsQuery(
            );

            LocationsResult locationsResult = await _mediator.Send(command);

            List<LocationDto>? locationsDto = locationsResult?.Locations?
                .Select(location => new LocationDto
                {
                    Id = location.Id,
                    Name = location.Name,
                    Description = location.Description
                })
                .ToList();

            var response = new GetLocationsResponse(
                locationsDto
            );

            return Ok(response);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch(Exception ex)
        {
            if (ex is IServiceException se)
            {
                return StatusCode((int)se.StatusCode, se.ErrorMessage);
            }
            else
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }

    [HttpGet("desks/{locationName}")]
    public async Task<IActionResult> GetDesksFromLocation(
        GetDesksFromLocationRequest request, 
        string locationName)
    {
        try{
            string role;
            if (User.HasClaim("Role", "admin"))
            {
                role = "admin";
            }
            else
            {
                role = "employee";
            }

            var command = new GetDesksInLocationQuery(
                locationName ?? request.Name
            );

            DesksResult desksResult = await _mediator.Send(command);

            List<DeskDto>? desksDto = desksResult?.desks?
                .Select(desk => new DeskDto
                {
                    Id = desk.Id,
                    LocationId = desk.LocationId.ToString(),
                    // only admins can see who reserved the desk
                    UserEmail = role == "admin" ? desk.UserEmail : null,
                    Available = desk.Available,
                    ReservationStartDate = desk.ReservationStartDate,
                    ReservationEndDate = desk.ReservationEndDate
                })
                .ToList();

            var response = new GetDesksResponse(
                desksDto
            );

            return Ok(response);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch(Exception ex)
        {
            if (ex is IServiceException se)
            {
                return StatusCode((int)se.StatusCode, se.ErrorMessage);
            }
            else
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }

    [HttpPut("{locationName}")]
    public async Task<IActionResult> UpdateLocation(
        UpdateLocationRequest request, 
        string locationName)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new UpdateLocationCommand(
                locationName,
                request.ChangedName,
                request.Description
            );

            LocationResult locationResult = await _mediator.Send(command);

            var response = new UpdateLocationResponse(
                locationResult.Location.Name
            );

            return Ok(response);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch(Exception ex)
        {
            if (ex is IServiceException se)
            {
                return StatusCode((int)se.StatusCode, se.ErrorMessage);
            }
            else
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }

    [HttpDelete("{locationName}")]
    public async Task<IActionResult> DeleteLocation(
        DeleteLocationRequest request, 
        string locationName)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new DeleteLocationCommand(
                locationName ?? request.Name
            );

            LocationResult locationResult = await _mediator.Send(command);

            var response = new DeleteLocationResponse(
                locationResult.Location.Name
            );

            return Ok(response);
        }
        catch (ValidationException vex)
        {
            return BadRequest(new { message = vex.Message });
        }
        catch(Exception ex)
        {
            if (ex is IServiceException se)
            {
                return StatusCode((int)se.StatusCode, se.ErrorMessage);
            }
            else
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}