using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class LocationRepository : ILocationRepository
{
    private static readonly List<Location> _desks = new();

    public void Add(Location location)
    {
        _desks.Add(location);
    }

    public User? GetUserByEmail(string email)
    {
        return null;
    }

}
