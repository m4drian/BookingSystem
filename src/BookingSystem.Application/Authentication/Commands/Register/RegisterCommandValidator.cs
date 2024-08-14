using FluentValidation;

namespace BookingSystem.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name must not be empty");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name must not be empty");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Bad email format");
        RuleFor(x => x.Role)
            .NotEmpty()
            .Custom((role, context) =>
            {
                if (role != "admin" && role != "employee")
                {
                    context.AddFailure("Role must be either 'admin' or 'employee'");
                }
            });
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(16)
            .MaximumLength(32)
            .WithMessage("Password must not be empty and be 16-32 characters long");
    }
}