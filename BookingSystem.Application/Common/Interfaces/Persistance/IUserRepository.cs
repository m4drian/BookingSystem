using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Common.Interfaces.Persistance;

public interface IUserRepository{

    User? GetUserByEmail(string email);

    string? GetUserRoleByEmail(string email);

    void Add(User user);
}