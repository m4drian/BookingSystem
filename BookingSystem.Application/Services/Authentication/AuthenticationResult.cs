using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Services.Authentication;

public record AuthenticationResult(
    User user,
    string Token
);