using BookingSystem.Application.Authentication.Common;
using MediatR;

namespace BookingSystem.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<AuthenticationResult>;