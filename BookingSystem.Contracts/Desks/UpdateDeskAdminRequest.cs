namespace BookingSystem.Contracts.Desks;

public record UpdateDeskAdminRequest
(
    int DeskId,
    
    string? UserEmail,

    bool? Available,

    DateTime? StartDate,

    DateTime? EndDate

);