using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using FluentValidation;
using MediatR;

namespace BookingSystem.Application.Common.Behaviors;

public class ValidateDesks 
    : IPipelineBehavior<CreateDeskCommand, DeskResult>
{
    private readonly IValidator<CreateDeskCommand> _validator;

    public ValidateDesks(IValidator<CreateDeskCommand> validator)
    {
        _validator = validator;
    }

    public async Task<DeskResult> Handle(CreateDeskCommand request, RequestHandlerDelegate<DeskResult> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if( !validationResult.IsValid )
        {
            throw new RegisterValidationException();
        }

        return await next();
    }
}