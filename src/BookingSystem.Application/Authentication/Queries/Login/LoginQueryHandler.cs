using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Helpers;

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

    public async Task<AuthenticationResult> Handle(
        LoginQuery query, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        LoginValidation(query);

        // check if user exists
        if(_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            // this will not share details with Api layer on purpose
            throw new UserNotFoundException();
        }

        // validate password
        if (user.Password != query.Password)
        {
            throw new InvalidPasswordException();
        }

        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    private void LoginValidation(LoginQuery query)
    {
        // Email Validation
        if (string.IsNullOrEmpty(query.Email) || !ValidateHelper.IsValidEmail(query.Email))
        {
            throw new ValidationException("Bad email format");
        }

        // Password Validation
        if (string.IsNullOrEmpty(query.Password) ||
            query.Password.Length < 16 || query.Password.Length > 32)
        {
            throw new ValidationException("Password must not be empty and be 16-32 characters long");
        }
    }
}