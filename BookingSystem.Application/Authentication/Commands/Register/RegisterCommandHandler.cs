using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Authentication.Common.Interfaces.Authentication;
using BookingSystem.Application.Authentication.Common.Interfaces.Persistance;
using BookingSystem.Application.Authentication.Common;
using BookingSystem.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Application.Common.Helpers;

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

        RegisterValidation(command);

        // check if user exists
        if(_userRepository.GetUserByEmail(command.Email) != null)
        {
            throw new DuplicateEmailException();
        }
        
        // check if user exists
        if(_userRepository.GetUserByEmail(command.Email) != null)
        {
            throw new DuplicateEmailException();
        }

        // create user, generate ID and persist to DB
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
        // Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    private void RegisterValidation(RegisterCommand command)
    {
        // First Name Validation
        if (string.IsNullOrEmpty(command.FirstName))
        {
            throw new ValidationException("First name must not be empty");
        }

        // Last Name Validation
        if (string.IsNullOrEmpty(command.LastName))
        {
            throw new ValidationException("Last name must not be empty");
        }

        // Email Validation
        if (string.IsNullOrEmpty(command.Email) || !ValidateHelper.IsValidEmail(command.Email))
        {
            throw new ValidationException("Bad email format");
        }

        // Role Validation
        if (string.IsNullOrEmpty(command.Role) ||
            (command.Role != "admin" && command.Role != "employee"))
        {
            throw new ValidationException("Role must be either 'admin' or 'employee'");
        }

        // Password Validation
        if (string.IsNullOrEmpty(command.Password) ||
            command.Password.Length < 16 || command.Password.Length > 32)
        {
            throw new ValidationException("Password must not be empty and be 16-32 characters long");
        }
    }
}