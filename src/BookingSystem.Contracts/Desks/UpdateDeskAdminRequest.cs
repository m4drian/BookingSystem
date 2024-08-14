namespace BookingSystem.Contracts.Desks;

public record UpdateDeskAdminRequest(
    string DeskId,
    string? UserEmail,
    bool? Available,
    DateTime? StartDate,
    DateTime? EndDate
);