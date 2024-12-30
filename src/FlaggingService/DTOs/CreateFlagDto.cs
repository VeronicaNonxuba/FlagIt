public class CreateFlagDto
{
    public Guid FlagId { get; set; }
    public Guid EstablishmentId { get; set; }
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public string? CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}
