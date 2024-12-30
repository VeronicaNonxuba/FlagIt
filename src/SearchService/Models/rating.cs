using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace SearchService.Models;

public class Rating : Entity
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid FlagId { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid EstablishmentId { get; set; }

    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid FlaggedBy { get; set; }
    public string? Username { get; set; }
    public DateTime FlaggedOn { get; set; }
    public string? Comments { get; set; }
    public string? Color { get; set; }
    public string? Significance { get; set; }
    public string? Owner { get; set; }
    public string? Address { get; set; }
    public string? EstablishmentStatus { get; set; }
    public string? EstablishmentStatusString { get; set; }
    public string? EstablishmentName { get; set; }
    public string? EstablishmentTypeName { get; set; }
}
