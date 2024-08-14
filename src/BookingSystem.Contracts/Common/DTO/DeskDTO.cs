namespace BookingSystem.Contracts.Common.DTO;

public record DeskDto
{
    public Guid Id { get; init; }
    public string LocationId { get; init; } = null!;
    public string? UserEmail { get; init; }
    public bool Available { get; init; }
    public DateTime? ReservationStartDate { get; init; }
    public DateTime? ReservationEndDate { get; init; }
}