using SearchService.Models;

namespace SearchService.Data;

public interface ISearchRepository
{
    Task<IEnumerable<Rating>> SearchItems(string searchTerm);
}
