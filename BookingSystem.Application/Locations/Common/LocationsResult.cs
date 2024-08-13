using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Locations.Common;

public record LocationsResult(
    List<Location>? Locations
);