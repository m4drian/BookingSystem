using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common.Interfaces.Persistance;

public interface IDeskRepository
{
    void Add(Desk desk, Location location);
    List<Desk>? GetAllDesks();
    // to check if exists
    Desk? GetDeskById(Guid deskId);
    void UpdateDeskAdmin(Desk desk);
    void UpdateAllDeskLocations(Location location);
    bool DidUserAlreadyBook(string email);
    void ReserveDeskEmployee(Desk desk);
    void DeleteDesk(Guid deskId);
}