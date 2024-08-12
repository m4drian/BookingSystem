using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Application.Services.Authentication.Common;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string role, string password)
    {
        // check if user exists
        if(_userRepository.GetUserByEmail(email) != null)
        {
            throw new DuplicateEmailException();
        }

        // create user generate ID and persist do DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Role = role,
            Password = password
        };

        _userRepository.Add(user);

        // create token
        //Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user, 
            token);
    }
}
