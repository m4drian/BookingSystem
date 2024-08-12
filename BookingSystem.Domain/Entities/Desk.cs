using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.Entities;

public class Desk{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public int LocationId { get; set; }

    public int UserId { get; set; }
    
    public bool available { get; set; }

    public DateTime startDate { get; set; }

    public DateTime endDate { get; set; }
}