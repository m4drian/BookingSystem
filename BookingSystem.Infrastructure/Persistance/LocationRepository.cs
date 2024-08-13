using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class LocationRepository : ILocationRepository
{
    private static readonly List<Location> _locations = new();

    public void Add(Location location)
    {
        _locations.Add(location);
        throw new NotImplementedException();
    }

    public void Delete(Location location)
    {
        throw new NotImplementedException();
    }

    public List<Location>? GetAllLocations()
    {
        throw new NotImplementedException();
    }

    public List<Desk>? GetDesksInLocation(string locationName)
    {
        throw new NotImplementedException();
    }

    public Location? GetLocationByName(string locationName)
    {
        throw new NotImplementedException();
    }

    public void Update(Location location)
    {
        throw new NotImplementedException();
    }
}
