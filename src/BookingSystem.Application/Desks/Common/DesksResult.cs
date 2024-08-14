using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Desks.Common;

public record DesksResult(
    List<Desk>? desks
);