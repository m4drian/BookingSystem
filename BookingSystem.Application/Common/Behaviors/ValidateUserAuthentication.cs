using BookingSystem.Application.Authentication.Commands.Register;
using BookingSystem.Application.Common.Errors;
using FluentValidation;
using MediatR;

namespace BookingSystem.Application.Authentication.Common.Behaviors;

public class ValidateRegisterCommandBehavior 
    : IPipelineBehavior<RegisterCommand, AuthenticationResult>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand request, 
        RequestHandlerDelegate<AuthenticationResult> next, 
        CancellationToken cancellationToken)
    {
        // before the handler
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if( !validationResult.IsValid )
        {
            throw new RegisterValidationException();
        }

        return await next();
    }
}