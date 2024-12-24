using System.Drawing;
using FlaggingService.Entities;
namespace FlaggingService.DTOs;

public class FlaggingDto
{
    public Guid FlagId { get; set; }
    public Guid EstablishmentId { get; set; }
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public string? Color { get; set; }
    public Significance? Significance { get; set; }
    public string? Owner { get; set; }
    public string? Address { get; set; }
    public Status? EstablishmentStatus { get; set; }
    public string? EstablishmentStatusString { get; set; }
    public string? EstablishmentName { get; set; }
    public string? EstablishmentTypeName { get; set; }
}
