using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common.Interfaces.Persistance;

public interface ILocationRepository
{
    User? GetUserByEmail(string email);
    void Add(Location location);
}