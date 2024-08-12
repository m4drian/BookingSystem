namespace BookingSystem.Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string firstName, string lastName, string email, string role, string password);

    AuthenticationResult Login(string email, string password);

}