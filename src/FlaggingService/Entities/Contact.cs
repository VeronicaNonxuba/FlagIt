using System.ComponentModel.DataAnnotations;

namespace FlaggingService.Entities;

public class Contact
{
    [Key]
    public Guid Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? MobileNo { get; set; }
    public string? TelephoneNo { get; set; }
    public Dictionary<string, string>? SocialsInfo { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsDeleted { get; set; } = false;

    //nav props
    public Establishment? Establishment { get; set; }
}
