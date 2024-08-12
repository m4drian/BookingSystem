using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Common;

public record AuthenticationResult(
    User user,
    string Token
);