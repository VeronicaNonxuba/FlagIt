using AutoMapper;
using FlaggingService.Data;
using FlaggingService.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlaggingController : ControllerBase
{
    private readonly FlaggingDbContext _context;
    private readonly IMapper _mapper;
    public FlaggingController(FlaggingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<FlaggingDto>>> GetFlaggedEstablishments()
    {
        var flags = await _context.Flagging
            .Include(x => x.Flag)
            .Include(y => y.Flagger)
            .Include(z => z.Establishment)
            .OrderBy(n => n.FlagId)
            .OrderBy(n => n.EstablishmentId)
            .ToListAsync();

        var flaggingDtos = _mapper.Map<List<FlaggingDto>>(flags);
        return Ok(flaggingDtos);
    }

    [HttpGet("{estId,flaggedBy,flagId}")]
    public async Task<ActionResult<FlaggingDto>> GetFlaggedEstablishment(Guid estId, Guid flaggedBy, Guid flagId)
    {
        var flag = await _context.Flagging
            .Where(i => i.EstablishmentId == estId
                && i.FlaggedBy == flaggedBy
                && i.FlagId == flagId)
            .Include(x => x.Flag)
            .Include(y => y.Flagger)
            .Include(z => z.Establishment)
            .OrderBy(n => n.FlagId)
            .OrderBy(n => n.EstablishmentId)
            .FirstOrDefaultAsync();

        if (flag == null)
        {
            return NotFound();
        }

        return _mapper.Map<FlaggingDto>(flag);
    }

}
