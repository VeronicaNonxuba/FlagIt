using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaggingService.Entities;
[Table("Users")]
public class Flagger
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string? Username { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public int FlagCount { get; set; }

    //nav props
    public ICollection<Flagging>? Flagging { get; set; }
}
