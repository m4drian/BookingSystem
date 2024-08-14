namespace BookingSystem.Contracts.Locations;

public record CreateLocationRequest(
    string Name,
    string? Description
);