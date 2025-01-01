using AutoMapper;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class SearchRepository : ISearchRepository
{
    private readonly IMapper _mapper;
    public SearchRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<QueryResponse> SearchItems(string searchTerm,
    int pageSize = 3, int pageNumber = 1)
    {
        QueryResponse result = new QueryResponse();
        try
        {
            var query = DB.PagedSearch<Rating>();
            query.Sort(x => x.Ascending(x => x.EstablishmentName));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }
            query.PageNumber(pageNumber);
            query.PageSize(pageSize);
            var queryResult = await query.ExecuteAsync();

            result.Results = queryResult.Results.ToList().AsReadOnly();
            result.TotalCount = queryResult.TotalCount;
            result.PageCount = queryResult.PageCount;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }


}
