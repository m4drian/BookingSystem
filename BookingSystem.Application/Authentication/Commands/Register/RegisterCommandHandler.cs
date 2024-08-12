using BookingSystem.Application.Common.Errors;
using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Application.Common;
using BookingSystem.Domain.Entities;
using MediatR;

namespace BookingSystem.Application.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, AuthenticationResult>
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator, 
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand command, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        // check if user exists
        if(_userRepository.GetUserByEmail(command.Email) != null)
        {
            throw new DuplicateEmailException();
        }

        // create user generate ID and persist do DB
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Role = command.Role,
            Password = command.Password
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