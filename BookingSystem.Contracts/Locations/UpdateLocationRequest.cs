namespace BookingSystem.Contracts.Locations;

public record UpdateLocationRequest
{
    string Name;

    string? ChangedName;

    string? Description;
}