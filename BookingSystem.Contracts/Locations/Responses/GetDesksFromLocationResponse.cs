using BookingSystem.Contracts.Common.DTO;

namespace BookingSystem.Contracts.Locations.Responses;

public record GetDesksFromLocationResponse
(

    List<DeskDto> Desks

);