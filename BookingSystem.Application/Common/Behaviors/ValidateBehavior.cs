using BookingSystem.Application.Common.Errors;
using FluentValidation;
using MediatR;

namespace BookingSystem.Application.Authentication.Common.Behaviors;

public class ValidateBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidateBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if ( _validator is null )
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if( !validationResult.IsValid )
        {
            throw new GenericValidationException();
        }

        var errorMessages = validationResult.Errors
            .Select(x => x.ErrorMessage)
            .ToList();

        // Combine error messages
        var errorMessage = string.Join(", ", errorMessages);

        if(errorMessages.Any())
        {
            throw new GenericValidationException();
        }

        return await next();
    }
}