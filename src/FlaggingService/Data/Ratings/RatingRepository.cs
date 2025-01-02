using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlaggingService.RequestHelpers;


namespace FlaggingService.Data;

public class RatingRepository : IRatingRepository
{
    private readonly IMapper _mapper;
    private readonly FlaggingDbContext _context;

    public RatingRepository(IMapper mapper, FlaggingDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Task DeleteFlaggingEntry(RequestItem requestObj)
    {
        throw new NotImplementedException();
    }

    public async Task<int> FlagAnEstablishment(Rating request)
    {
        var requestObj = _mapper.Map<Rating>(request);
        _context.Ratings.Add(requestObj);
        return await _context.SaveChangesAsync();
    }

    public async Task<FlaggingDto> GetFlaggingDtoById(Guid estId, Guid flagId, Guid flaggerId)
    {
        //fetch the entity first, make sure to fetch the navigation properties too (using include)
        var result = await _context.Ratings
                        .Include(f => f.Flag)
                        .Include(e => e.Establishment)
                        .Include(fl => fl.User)
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

    public async Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj)
    {
        var query = _context.Ratings
            .Include(x => x.Flag)
            .Include(y => y.User)
            .Include(z => z.Establishment)
            .OrderBy(n => n.FlagId)
            .OrderBy(n => n.EstablishmentId)
            .AsQueryable();

        query = query.Where(x => x.FlaggedOn.CompareTo(Helper.ConvertToUtc(requestObj.FlaggedOn).ToUniversalTime()) > 0);
        query = query.Where(x => x.EstablishmentId == requestObj.EstablishmentId);
        query = query.Where(x => x.FlagId == requestObj.FlagId);
        query = query.Where(x => x.FlaggedBy == requestObj.UserId);

        var result = await query.ProjectTo<FlaggingDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        if (result == null)
        {
            throw new Exception("Flagging not found");
        }
        return result;
    }

    public async Task<List<FlaggingDto>> GetFlaggings(string? date)
    {
        var query = _context.Ratings
            .Include(x => x.Flag)
            .Include(y => y.User)
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
