using BookingSystem.Application.Authentication.Commands.Register;
using BookingSystem.Application.Authentication.Queries.Login;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Errors;

namespace BookingSystem.Api.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try{
            var command = new RegisterCommand(
                request.FirstName, 
                request.LastName,
                request.Email,
                request.Role,
                request.Password);

            AuthenticationResult authResult = await _mediator.Send(command);

            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.user.Role,
                authResult.Token
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try{
            var query = new LoginQuery(request.Email, request.Password);
            AuthenticationResult authResult = await _mediator.Send(query);

            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.user.Role,
                authResult.Token
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