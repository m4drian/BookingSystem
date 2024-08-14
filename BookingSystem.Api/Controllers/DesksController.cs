using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using BookingSystem.Application.Desks.Queries;
using BookingSystem.Application.Desks.Query;
using BookingSystem.Contracts.Common.DTO;
using BookingSystem.Contracts.Desks;
using BookingSystem.Contracts.Desks.Responses;
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
    public async Task<IActionResult> CreateDesk(
        CreateDeskRequest request, 
        string locationName)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new CreateDeskCommand(
                locationName,
                request.UserEmail,
                request.Available,
                request.StartDate,
                request.EndDate
            );

            DeskResult deskResult = await _mediator.Send(command);

            var response = new CreateDeskResponse(
                deskResult.desk.Location.Name
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
    public async Task<IActionResult> GetDesks(GetDesksRequest request)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new GetDesksQuery(
            );

            DesksResult desksResult = await _mediator.Send(command);

            List<DeskDto>? desksDto = desksResult?.desks?
                .Select(desk => new DeskDto
                {
                    Id = desk.Id,
                    LocationId = desk.LocationId.ToString(),
                    UserEmail = desk.UserEmail,
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

    [HttpGet("{deskId}")]
    public async Task<IActionResult> GetDeskReservationStatus(
        GetDeskReservationRequest request, 
        string deskId)
    {
        try{
            var command = new GetDeskReservationQuery(
                deskId ?? request.Id
            );

            ReservationResult reservationResult = await _mediator.Send(command);

            var response = new GetDeskReservationResponse(
                reservationResult.Available
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

    [HttpPut("reservation/{deskId}")]
    public async Task<IActionResult> UpdateDeskAsEmployee(
        UpdateDeskEmployeeRequest request, 
        string deskId)
    {
        try{
            if (!User.HasClaim("Role", "employee"))
            { return Unauthorized(request); }

            var command = new UpdateDeskEmployeeCommand(
                deskId ?? request.DeskId,
                request.UserEmail,
                request.Available,
                request.StartDate,
                request.EndDate
            );

            DeskResult deskResult = await _mediator.Send(command);

            var response = new UpdateDeskResponse(
                deskResult.desk.Available == false ? "Successfully booked selected desk." : "Selected desk is no longer booked."
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

    [HttpPut("reservation/admin/{deskId}")]
    public async Task<IActionResult> UpdateDeskAsAdmin(
        UpdateDeskAdminRequest request, 
        string deskId)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new UpdateDeskAdminCommand(
                deskId ?? request.DeskId,
                request.UserEmail,
                request.Available,
                request.StartDate,
                request.EndDate
            );

            DeskResult deskResult = await _mediator.Send(command);

            var response = new UpdateDeskResponse(
                JsonSerializer.Serialize(deskResult.desk).ToString()
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

    [HttpDelete("{deskId}")]
    public async Task<IActionResult> DeleteDesk(
        DeleteDeskRequest request, 
        string deskId)
    {
        try{
            if (!User.HasClaim("Role", "admin"))
            { return Unauthorized(request); }

            var command = new DeleteDeskCommand(
                deskId ?? request.Id
            );

            DeskResult deskResult = await _mediator.Send(command);

            var response = new DeleteDeskResponse(
                "Successfully deleted desk from location: " + deskResult.desk.Location.Name
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