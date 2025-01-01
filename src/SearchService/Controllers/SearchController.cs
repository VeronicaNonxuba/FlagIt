using Microsoft.AspNetCore.Mvc;
using SearchService.Data;
using SearchService.RequestHelpers;

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
    public async Task<ActionResult> SearchItems([FromQuery] SearchParams searchParams)
    {
        if (searchParams == null)
        {
            return BadRequest("Search parameters are required");
        }
        var result = await _searchRepository.SearchItems(searchParams);
        return Ok(result);
    }
}
