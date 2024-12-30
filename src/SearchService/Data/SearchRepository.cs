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
    public Task<IEnumerable<Rating>> SearchItems(string searchTerm)
    {
        Task<IEnumerable<Rating>> result = Task.FromResult(Enumerable.Empty<Rating>());
        try
        {
            //var query = DB.
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return result;
    }
}
