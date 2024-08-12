using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookingSystem.Domain.Entities;

public class Location{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = null!;

    [AllowNull]
    [Required(AllowEmptyStrings = true)]
    public string? Description { get; set; }
    
    [AllowNull]
    public List<Desk>? Desks { get; set; } = null!;
}