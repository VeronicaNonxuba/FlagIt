using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace FlaggingService.Entities;

[Table("Flags")]
public class Flag
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Color { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public Significance Significance { get; set; } = Significance.Okay;

    //nav props
    public ICollection<Flagging>? Flagging { get; set; }
}
