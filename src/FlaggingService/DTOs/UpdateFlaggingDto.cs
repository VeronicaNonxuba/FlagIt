using System.ComponentModel.DataAnnotations;

namespace FlaggingService.DTOs;

public class UpdateFlaggingDto
{
    [Required]
    public Guid FlagId { get; set; }

    [Required]
    public Guid EstablishmentId { get; set; }

    [Required]
    public Guid FlaggedBy { get; set; }

    [Required]
    public string? Comments { get; set; }

    [Required]
    public int FlagCount { get; set; }
}
