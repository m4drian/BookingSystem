namespace BookingSystem.Contracts.Desks;

public record CreateDeskRequest
{
    string LocationName;
    
    string? UserEmail;

    bool Available;

    DateTime? StartDate;

    DateTime? EndDate;

}