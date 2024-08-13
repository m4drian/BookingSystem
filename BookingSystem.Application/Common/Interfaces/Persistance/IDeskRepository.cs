using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common.Interfaces.Persistance;

public interface IDeskRepository
{
    void Add(Desk desk, Guid locationId);
    List<Desk>? GetAllDesks();
    // to check if exists
    Desk? GetDeskById(Guid deskId);
    void UpdateDeskAdmin(Desk desk);
    void ReserveDeskEmployee(Desk desk);
    void DeleteDesk(Guid deskId);
}