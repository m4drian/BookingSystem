using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Common.Interfaces.Persistance;

public interface ILocationRepository{

    User? GetUserByEmail(string email);

    void Add(Location location);
}