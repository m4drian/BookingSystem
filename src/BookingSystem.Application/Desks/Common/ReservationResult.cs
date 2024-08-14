using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Desks.Common;

public record ReservationResult(
    bool Available
);