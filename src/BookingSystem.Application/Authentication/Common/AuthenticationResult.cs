using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token
);