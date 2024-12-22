using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaggingService.Entities;

public class Flagging
{
    [ForeignKey("Flag")]
    public Guid FlagId { get; set; }

    [ForeignKey("Establishment")]
    public Guid EstablishmentId { get; set; }

    [ForeignKey("Flagger")]
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public int FlagCount { get; set; }

    //navigation properties
    [Required]
    public Flagger? Flagger { get; set; }

    [Required]
    public Flag? Flag { get; set; }

    [Required]
    public Establishment? Establishment { get; set; }
}
