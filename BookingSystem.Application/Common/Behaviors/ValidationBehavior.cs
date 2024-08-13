/*using BookingSystem.Application.Authentication.Common.Errors;
using BookingSystem.Application.Desks.Commands;
using BookingSystem.Application.Desks.Common;
using FluentValidation;
using MediatR;

namespace BookingSystem.Application.Common.Behaviors;

public class ValidationBehavior : 
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IValidator<CreateDeskCommand> _validator;

    public ValidationBehavior(IValidator<CreateDeskCommand> validator)
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}*/