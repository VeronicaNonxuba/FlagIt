using Microsoft.AspNetCore.Mvc;

using FlaggingService.Data;

namespace FlaggingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlaggingController : ControllerBase
{
    private readonly IFlaggingRepository _service;
    public FlaggingController(IFlaggingRepository service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<FlaggingDto>>> GetFlaggedEstablishments()
    {
        return Ok(await _service.GetFlaggings("2024-12-28"));
    }

    [HttpGet("{estId,flaggedBy,flagId}")]
    public async Task<ActionResult<FlaggingDto>> GetFlaggedEstablishment(Guid estId, Guid flaggedBy, Guid flagId)
    {
        var request = new RequestItem
        {
            EstablishmentId = estId,
            FlaggerId = flaggedBy,
            FlagId = flagId
        };

        return Ok(await _service.GetFlaggingDtoById(request));
    }

    [HttpPost]
    public async Task<ActionResult<bool>> FlagAnEstablishment(CreateFlagDto request)
    {
        var response = await _service.FlagAnEstablishment(request) > 0;

        if (!response) return BadRequest("Could not save changes to the database");

        return Ok(true);
    }
}
