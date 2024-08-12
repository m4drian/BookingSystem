using System.Reflection;
using BookingSystem.Application.Authentication.Commands.Register;
using BookingSystem.Application.Common;
using BookingSystem.Application.Common.Behaviors;
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

        services.AddScoped<
            IPipelineBehavior<RegisterCommand, AuthenticationResult>, 
            ValidateRegisterCommandBehavior>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}