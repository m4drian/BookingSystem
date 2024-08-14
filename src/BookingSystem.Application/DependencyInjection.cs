using System.Reflection;
using BookingSystem.Application.Authentication.Commands.Register;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            config => config.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly
            )
        );

        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();

        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}