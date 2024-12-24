using AutoMapper;
using FlaggingService.Data;
using FlaggingService.DTOs;
using FlaggingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Services;

public class FlaggingService
{
    private readonly IMapper _mapper;
    private readonly FlaggingDbContext _context;

    public FlaggingService(IMapper mapper, FlaggingDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public FlaggingDto GetFlaggingDtoById(Guid id)
    {
        //fetch the entity first, make sure to fetch the navigation properties too (using include)
        var flagging = _context.Flagging.Include(f => f.Flag).Include(e => e.Establishment).Include(fl => fl.Flagger)
        .FirstOrDefault(f => f.EstablishmentId == id);

        if (flagging is null)
        {
            throw new Exception("flagging was not found");
        }
        var flaggingDto = _mapper.Map<FlaggingDto>(flagging);
        return flaggingDto;
    }
}
