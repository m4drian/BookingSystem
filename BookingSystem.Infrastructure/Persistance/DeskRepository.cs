using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class DeskRepository : IDeskRepository
{
    private static readonly List<Desk> _desks = new();

    public void Add(Desk desk, Guid locationId)
    {
        desk.LocationId = locationId;
        _desks.Add(desk);
    }

    public void DeleteDesk(Guid deskId)
    {
        var deskToRemove = _desks.FirstOrDefault(d => d.Id == deskId);
        if (deskToRemove != null)
        {
            _desks.Remove(deskToRemove);
        }
    }

    public List<Desk>? GetAllDesks()
    {
        return _desks.ToList();
    }

    public Desk? GetDeskById(Guid deskId)
    {
        return _desks.FirstOrDefault(d => d.Id == deskId);
    }

    public void ReserveDeskEmployee(Desk desk)
    {
        var existingDesk = _desks.FirstOrDefault(d => d.Id == desk.Id);
        if (existingDesk != null)
        {
            existingDesk.UserEmail = desk.UserEmail;
            existingDesk.ReservationStartDate = desk.ReservationStartDate;
            existingDesk.ReservationEndDate = desk.ReservationEndDate;
            existingDesk.Available = desk.Available;
        }
    }

    public void UpdateDeskAdmin(Desk desk)
    {
        var existingDesk = _desks.FirstOrDefault(d => d.Id == desk.Id);
        if (existingDesk != null)
        {
            existingDesk.UserEmail = desk.UserEmail;
            existingDesk.Available = desk.Available;
            existingDesk.ReservationStartDate = desk.ReservationStartDate;
            existingDesk.ReservationEndDate = desk.ReservationEndDate;
        }
    }
}
