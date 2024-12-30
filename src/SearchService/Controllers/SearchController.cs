using Microsoft.AspNetCore.Mvc;
using SearchService.Data;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchRepository _searchRepository;
    public SearchController(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }
    [HttpGet]
    public async Task<ActionResult> SearchItems(string searchTerm)
    {
        var result = await _searchRepository.SearchItems(searchTerm);
        return Ok(result);
    }
}
