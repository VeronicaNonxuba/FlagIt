public class FlaggingDto
{
    public Guid FlagId { get; set; }
    public Guid EstablishmentId { get; set; }
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public string? Color { get; set; }
    public string? Significance { get; set; }
    public string? Owner { get; set; }
    public string? Address { get; set; }
    public string? EstablishmentStatus { get; set; }
    public string? EstablishmentName { get; set; }
    public string? EstablishmentTypeName { get; set; }
}
