using BookingSystem.Application.Services.Authentication.Common;

namespace BookingSystem.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    AuthenticationResult Register(string firstName, string lastName, string email, string role, string password);

}