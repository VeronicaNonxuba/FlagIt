using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class RatingUpdated
{
    [Required]
    public Guid FlagId { get; set; }

    [Required]
    public Guid EstablishmentId { get; set; }

    [Required]
    public Guid FlaggedBy { get; set; }

    [Required]
    public DateTime FlaggedOn { get; set; }

    public string? Comments { get; set; }

    public int? FlagCount { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
}
