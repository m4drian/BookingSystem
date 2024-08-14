using BookingSystem.Application.Authentication.Common.Interfaces.Services;
using FluentValidation;

namespace BookingSystem.Application.Desks.Commands.Validation;

public class CreateDeskCommandValidator : AbstractValidator<CreateDeskCommand>
{
    private readonly IDateTimeProvider _clock;

    public CreateDeskCommandValidator(IDateTimeProvider clock)
    {
        _clock = clock;

        RuleFor(x => x.LocationName).NotEmpty().WithMessage("Location name is required");
        RuleFor(x => x.UserEmail).EmailAddress().WithMessage("Bad email format");
        RuleFor(x => x.Available).NotEmpty().WithMessage("Available flag is required");
        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(_clock.UtcNow)
            .WithMessage("Start date must be in the future");
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to start date");
    }
}