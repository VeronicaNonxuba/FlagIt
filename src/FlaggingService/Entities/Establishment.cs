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
    public List<Guid>? ContactIds { get; set; }
    public string? Address { get; set; }
    public Status Status { get; set; } = Status.Active;
    public string? CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public int FlagCount { get; set; }

    //navigation properties
    [Required]
    public EstablishmentType? EstType { get; set; }

    public ICollection<Contact>? Contact { get; set; }

    public ICollection<Rating>? Rating { get; set; }

}
