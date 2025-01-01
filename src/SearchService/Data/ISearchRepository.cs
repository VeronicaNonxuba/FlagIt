using SearchService.Models;

namespace SearchService.Data;

public interface ISearchRepository
{
    Task<QueryResponse> SearchItems(string searchTerm,
    int pageSize = 3, int pageNumber = 1);
}
