namespace BookingSystem.Contracts.Locations;

public record UpdateLocationRequest(
    string? ChangedName,
    string? Description
);