using FluentValidation;

namespace BookingSystem.Application.Authentication.Commands.Register;

public class LoginQueryValidator : AbstractValidator<RegisterCommand>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Bad email format");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(16)
            .MaximumLength(32)
            .WithMessage("Password must not be empty and be 16-32 characters long");
    }
}