using System.ComponentModel.DataAnnotations;

namespace FlaggingService.Entities;

public class EstablishmentType
{
    [Key]
    public Guid Id { get; set; }
    public string? Description { get; set; }

    [Required]
    public string? Name { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; } = Status.Active;

    //navigation properties
    public ICollection<Establishment>? Establishments { get; set; }
}
