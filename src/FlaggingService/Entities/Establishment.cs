using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaggingService.Entities;

[Table("Establishments")]
public class Establishment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [ForeignKey("EstType")]
    public Guid TypeId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? Owner { get; set; }
    public int ContactId { get; set; }
    public string? Address { get; set; }
    public Status Status { get; set; } = Status.Active;

    //navigation properties
    [Required]
    public EstablishmentType? EstType { get; set; }

    public ICollection<Flagging>? Flagging { get; set; }

}
