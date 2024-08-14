namespace BookingSystem.Contracts.Desks;

public record UpdateDeskEmployeeRequest(
    string DeskId,
    string UserEmail,
    bool Available,
    DateTime StartDate,
    DateTime? EndDate
);