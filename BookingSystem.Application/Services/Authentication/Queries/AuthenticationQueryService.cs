using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Application.Services.Authentication.Common;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // check if user exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email doesn't exist.");
        }

        // validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid password.");
        }

        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
