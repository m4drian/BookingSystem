using BookingSystem.Application.Services.Authentication.Common;

namespace BookingSystem.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);
}