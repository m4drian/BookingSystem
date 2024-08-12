using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}