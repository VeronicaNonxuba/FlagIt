using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace FlaggingService.Data;

public class FlaggingRepository : IFlaggingRepository
{
    private readonly IMapper _mapper;
    private readonly FlaggingDbContext _context;

    public FlaggingRepository(IMapper mapper, FlaggingDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Task DeleteFlaggingEntry(RequestItem requestObj)
    {
        throw new NotImplementedException();
    }

    public async Task<int> FlagAnEstablishment(CreateFlagDto request)
    {
        var requestObj = _mapper.Map<Flagging>(request);
        _context.Flagging.Add(requestObj);
        return await _context.SaveChangesAsync();
    }

    public async Task<FlaggingDto> GetFlaggingDtoById(Guid estId, Guid flagId, Guid flaggerId)
    {
        //fetch the entity first, make sure to fetch the navigation properties too (using include)
        var result = await _context.Flagging
                        .Include(f => f.Flag)
                        .Include(e => e.Establishment)
                        .Include(fl => fl.Flagger)
                        .Where(x => x.FlaggedOn.CompareTo(DateTime.Now.AddDays(-1)) > 0)
                        .FirstOrDefaultAsync(f => f.EstablishmentId == estId
                                   && f.FlagId == flagId
                                   && f.FlaggedBy == flaggerId);

        if (result is null)
        {
            throw new Exception("flagging was not found");
        }
        var flaggingDto = _mapper.Map<FlaggingDto>(result);
        return flaggingDto;
    }

    public Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FlaggingDto>> GetFlaggings(string? date)
    {
        var query = _context.Flagging
            .Include(x => x.Flag)
            .Include(y => y.Flagger)
            .Include(z => z.Establishment)
            .OrderBy(n => n.FlagId)
            .OrderBy(n => n.EstablishmentId)
            .AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.FlaggedOn.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }
        return await query.ProjectTo<FlaggingDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

}
