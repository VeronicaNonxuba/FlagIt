namespace SearchService.RequestHelpers;

public class SearchParams
{
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public string? OrderBy { get; set; }
    public string? FilterBy { get; set; }
    public string? Username { get; set; }
    public string? EstablishmentTypeName { get; set; }
    public string? EstablishmentStatus { get; set; }
    public string? EstablishmentName { get; set; }
}
