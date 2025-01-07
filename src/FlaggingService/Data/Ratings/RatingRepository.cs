using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlaggingService.RequestHelpers;
using Microsoft.Extensions.Logging;


namespace FlaggingService.Data;

public class RatingRepository : IRatingRepository
{
    private readonly IMapper _mapper;
    private readonly FlaggingDbContext _context;
    private readonly ILogger<RatingRepository> _logger;
    public RatingRepository(IMapper mapper, FlaggingDbContext context,
    ILogger<RatingRepository> logger)
    {
        _context = context;
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> DeleteRatingEntry(RequestItem requestObj)
    {
        _logger.LogInformation("Attempting to delete rating entry with values: FlagId={FlagId}, UserId={UserId}, EstablishmentId={EstablishmentId}, FlaggedOn={FlaggedOn}",
            requestObj.FlagId, requestObj.FlaggedBy, requestObj.EstablishmentId, requestObj.FlaggedOn);

        try
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(x => x.FlaggedBy == requestObj.FlaggedBy
                                         && x.FlagId == requestObj.FlagId
                                         && x.EstablishmentId == requestObj.EstablishmentId
                                         && x.FlaggedOn == requestObj.FlaggedOn);

            if (rating == null)
            {
                _logger.LogError("No rating was found with the given criteria");
                throw new Exception("flagging was not found");
            }

            rating.IsDeleted = true;
            _context.Ratings.Update(rating);
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving rating");
            throw;
        }
    }

    public async Task<int> FlagAnEstablishment(Rating request)
    {
        try
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(x => x.FlaggedBy == request.FlaggedBy
                                         && x.FlagId == request.FlagId
                                         && x.EstablishmentId == request.EstablishmentId
                                         && x.FlaggedOn == request.FlaggedOn);

            if (rating != null)
            {
                _logger.LogError("Rating already exists with the given criteria");
                throw new Exception("Rating already exists");
            }

            _context.Ratings.Add(request);
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving rating");
            throw;
        }
    }
    public async Task<FlaggingDto> GetFlaggingDtoById(RequestItem requestObj)
    {
        try
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
            query = query.Where(x => x.FlaggedBy == requestObj.FlaggedBy);

            var result = await query.ProjectTo<FlaggingDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("Flagging not found");
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching rating");
            throw;
        }
    }

    public async Task<List<FlaggingDto>> GetRatings(string? date)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching ratings");
            throw;
        }
    }
}
