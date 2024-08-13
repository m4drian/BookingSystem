using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Authentication.Common.Interfaces.Persistance;

public interface IDeskRepository
{
    User? GetUserByEmail(string email);
    void Add(Desk desk);
}