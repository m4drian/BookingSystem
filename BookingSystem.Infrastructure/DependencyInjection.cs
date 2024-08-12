using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Application.Common.Interfaces.Persistance;
using BookingSystem.Application.Common.Interfaces.Services;
using BookingSystem.Infrastructure.Authentication;
using BookingSystem.Infrastructure.Persistance;
using BookingSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

}