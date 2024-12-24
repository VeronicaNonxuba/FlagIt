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
    public string? CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    //navigation properties
    public ICollection<Establishment>? Establishments { get; set; }
}
