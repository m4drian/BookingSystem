using BookingSystem.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }

}