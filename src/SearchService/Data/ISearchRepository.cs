using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Data;

public interface ISearchRepository
{
    Task<QueryResponse> SearchItems(SearchParams searchParams);
}
