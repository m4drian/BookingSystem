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

    public void Update(Location location, string? changedName, string? description)
    {
        var existingLocation = _locations.FirstOrDefault(l => l.Name == location.Name);
        if (existingLocation == null)
        { 
            return; 
        }

        if(changedName is not null)
        {
            existingLocation.Name = changedName;
        }
        existingLocation.Description = description;

        foreach (var desk in existingLocation.Desks)
        {
            desk.LocationId = existingLocation.Id;
            desk.Location = existingLocation;
        }
    }
}
