using BookingSystem.Contracts.Common.DTO;

namespace BookingSystem.Contracts.Locations.Responses;

public record LocationsResponse(
    List<LocationDto> Locations
);