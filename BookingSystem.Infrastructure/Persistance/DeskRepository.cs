using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Persistance;

public class DeskRepository : IDeskRepository
{
    private static readonly List<Desk> _desks = new();

    public void Add(Desk desk)
    {
        _desks.Add(desk);
    }

    public User? GetUserByEmail(string email)
    {
        return null;
    }

}
