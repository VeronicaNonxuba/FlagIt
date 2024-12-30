namespace FlaggingService.Entities;

public class RequestItem
{
    public Guid FlagId { get; set; }
    public Guid EstablishmentId { get; set; }
    public Guid FlaggerId { get; set; }
    public Status Status { get; set; } = Status.Active;
}
