namespace SearchService.Models;

public class QueryResponse
{
    public IReadOnlyList<Rating>? Results { get; set; }
    public int PageCount { get; set; }
    public long TotalCount { get; set; }
}
