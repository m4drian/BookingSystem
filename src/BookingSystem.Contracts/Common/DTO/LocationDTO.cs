namespace BookingSystem.Contracts.Common.DTO;

public record LocationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
}