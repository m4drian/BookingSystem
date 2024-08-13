using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Common.Interfaces.Persistance;

public interface IDeskRepository{

    User? GetUserByEmail(string email);

    void Add(Desk desk);
}