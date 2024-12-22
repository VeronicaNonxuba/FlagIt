using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaggingService.Entities;
[Table("Users")]
public class Flagger
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string? Username { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    //nav props
    public ICollection<Flagging>? Flagging { get; set; }
}
