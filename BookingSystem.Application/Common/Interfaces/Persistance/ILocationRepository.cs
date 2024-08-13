using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common.Interfaces.Persistance;

public interface ILocationRepository
{
    void Add(Location location);
    // to check if exists
    Location? GetLocationByName(string locationName);
    List<Location>? GetAllLocations();
    List<Desk>? GetDesksInLocation(string locationName); 
    void Update(Location location);
    void Delete(Location location);
}