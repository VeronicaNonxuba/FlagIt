namespace Contracts;

public class RatingDeleted
{
    public Guid FlagId { get; set; }
    public Guid EstablishmentId { get; set; }
    public Guid FlaggedBy { get; set; }
    public DateTime FlaggedOn { get; set; }
}
