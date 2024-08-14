using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookingSystem.Domain.Entities;

public class Desk
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid LocationId { get; set; }

    [Required]
    [ForeignKey(nameof(LocationId))]
    public required Location Location { get; set; }

    [AllowNull]
    public string? UserEmail { get; set; }
    
    [Required]
    public bool Available { get; set; } = true;

    [AllowNull]
    public DateTime? ReservationStartDate { get; set; }

    [AllowNull]
    public DateTime? ReservationEndDate { get; set; }
}