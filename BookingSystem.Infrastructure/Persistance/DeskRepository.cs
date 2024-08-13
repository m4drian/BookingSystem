using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class DeskRepository : IDeskRepository
{
    private static readonly List<Desk> _desks = new();

    public void Add(Desk desk, Guid locationId)
    {
        _desks.Add(desk);
        throw new NotImplementedException();
    }

    public void DeleteDesk(Guid deskId)
    {
        throw new NotImplementedException();
    }

    public List<Desk>? GetAllDesks()
    {
        throw new NotImplementedException();
    }

    public Desk? GetDeskById(Guid deskId)
    {
        throw new NotImplementedException();
    }

    public void ReserveDeskEmployee(Desk desk)
    {
        throw new NotImplementedException();
    }

    public void UpdateDeskAdmin(Desk desk)
    {
        throw new NotImplementedException();
    }
}
