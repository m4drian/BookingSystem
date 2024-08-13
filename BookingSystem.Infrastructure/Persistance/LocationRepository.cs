using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class LocationRepository : ILocationRepository
{
    private static readonly List<Location> _locations = new();

    public void Add(Location location)
    {
        _locations.Add(location);
    }

    public void Delete(Location location)
    {
        var locationToRemove = _locations.FirstOrDefault(l => l.Name == location.Name);
        if (locationToRemove != null)
        {
            _locations.Remove(locationToRemove);
        }
    }

    public List<Location>? GetAllLocations()
    {
        return _locations.ToList();
    }

    public List<Desk>? GetDesksInLocation(string locationName)
    {
        var location = _locations.FirstOrDefault(l => l.Name == locationName);
        return location?.Desks.ToList() ?? new List<Desk>();
    }

    public Location? GetLocationByName(string locationName)
    {
        return _locations.FirstOrDefault(l => l.Name == locationName);
    }

    public void Update(Location location)
    {
        var existingLocation = _locations.FirstOrDefault(l => l.Id == location.Id);
        if (existingLocation != null)
        {
            existingLocation.Name = location.Name;
            existingLocation.Description = location.Description;
        }
    }
}
