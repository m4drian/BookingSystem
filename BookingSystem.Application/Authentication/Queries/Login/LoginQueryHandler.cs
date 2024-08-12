using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Application.Common;
using BookingSystem.Domain.Entities;
using MediatR;

namespace BookingSystem.Application.Authentication.Queries.Login;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, AuthenticationResult>
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator, 
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
                // check if user exists
        if(_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new Exception("User with given email doesn't exist.");
        }

        // validate password
        if (user.Password != query.Password)
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