using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaggingService.Entities;

public class Rating
{
    [ForeignKey("Flag")]
    public Guid FlagId { get; set; }

    [ForeignKey("Establishment")]
    public Guid EstablishmentId { get; set; }

    [ForeignKey("Flagger")]
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    //navigation properties
    [Required]
    public User? User { get; set; }

    [Required]
    public Flag? Flag { get; set; }

    [Required]
    public Establishment? Establishment { get; set; }
}
