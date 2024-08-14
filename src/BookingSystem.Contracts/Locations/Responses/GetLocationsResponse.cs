using BookingSystem.Contracts.Common.DTO;

namespace BookingSystem.Contracts.Locations.Responses;

public record GetLocationsResponse(
    List<LocationDto>? Locations
);