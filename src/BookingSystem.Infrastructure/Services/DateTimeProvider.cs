using BookingSystem.Application.Authentication.Common.Interfaces.Services;

namespace BookingSystem.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}