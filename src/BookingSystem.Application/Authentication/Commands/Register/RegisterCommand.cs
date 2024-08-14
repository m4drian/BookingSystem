using BookingSystem.Application.Authentication.Common;
using MediatR;

namespace BookingSystem.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Password
) : IRequest<AuthenticationResult>;