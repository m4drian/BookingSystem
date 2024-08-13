namespace BookingSystem.Contracts.Desks;

public record UpdateDeskEmployeeRequest(
    int DeskId,
    string UserEmail,
    DateTime StartDate,
    DateTime? EndDate
);