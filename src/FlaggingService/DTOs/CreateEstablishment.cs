namespace FlaggingService.DTOs;

public class CreateEstablishmentDto
{
    public Guid TypeId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? Owner { get; set; }
    public List<Guid>? ContactIds { get; set; }
    public string? Address { get; set; }
}
