using FluentValidation;

namespace BookingSystem.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();;
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
            .MaximumLength(32);
    }
}