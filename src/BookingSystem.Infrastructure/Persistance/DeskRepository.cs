using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class DeskRepository : IDeskRepository
{
    private static readonly List<Desk> _desks = new();

    public void Add(Desk desk, Location location)
    {
        desk.LocationId = location.Id;
        desk.Location = location;
        location.Desks.Add(desk);
        _desks.Add(desk);
    }

    public void DeleteDesk(Guid deskId)
    {
        var deskToRemove = _desks.FirstOrDefault(d => d.Id == deskId);
        if (deskToRemove != null)
        {
            deskToRemove.Location.Desks.Remove(deskToRemove);
            _desks.Remove(deskToRemove);
        }
    }

    public List<Desk>? GetAllDesks()
    {
        return _desks.ToList();
    }

    public bool DidUserAlreadyBook(string email)
    {
        var exists = _desks.FirstOrDefault(d => d.UserEmail == email);
        if (exists != null)
        {
            return true;
        }

        return false;
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

    public void UpdateAllDeskLocations(Location location)
    {
        _desks.ForEach(desk => {desk.Location = location; desk.LocationId = location.Id; });
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
