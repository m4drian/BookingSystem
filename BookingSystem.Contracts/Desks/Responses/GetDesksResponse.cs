using BookingSystem.Contracts.Common.DTO;

namespace BookingSystem.Contracts.Desks.Responses;

public record GetDesksResponse(
    List<DeskDto> Desks
);