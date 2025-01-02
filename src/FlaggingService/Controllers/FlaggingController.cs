using Microsoft.AspNetCore.Mvc;
using FlaggingService.Data;
using MassTransit;
using AutoMapper;
using Contracts;
using FlaggingService.Data.Users;
using FlaggingService.RequestHelpers;

namespace FlaggingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlaggingController : ControllerBase
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IEstablishmentRepository _establishmentRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IFlagRepository _flagRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public FlaggingController(IMapper mapper, IRatingRepository ratingRepository,
    IPublishEndpoint publishEndpoint, IUsersRepository usersRepository,
    IEstablishmentRepository establishmentRepository,
    IFlagRepository flagRepository)
    {
        _ratingRepository = ratingRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _establishmentRepository = establishmentRepository;
        _usersRepository = usersRepository;
        _flagRepository = flagRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<FlaggingDto>>> GetFlaggedEstablishments()
    {
        return Ok(await _ratingRepository.GetFlaggings("2024-12-28"));
    }

    [HttpGet("{estId,flaggedBy,flagId}")]
    public async Task<ActionResult<FlaggingDto>> GetFlaggedEstablishment(Guid estId, Guid flaggedBy, Guid flagId, DateTime flaggedOn)
    {
        var request = new RequestItem
        {
            EstablishmentId = estId,
            UserId = flaggedBy,
            FlagId = flagId,
            FlaggedOn = flaggedOn
        };

        return Ok(await _ratingRepository.GetFlaggingDtoById(request));
    }

    [HttpPost]
    public async Task<ActionResult<FlaggingDto>> FlagAnEstablishment([FromBody] CreateFlagDto request)
    {
        try
        {
            var rating = await PrepareRequest(request);

            //4. Save to database
            var response = await _ratingRepository.FlagAnEstablishment(rating) > 0;

            //5. Return error if save failed
            if (!response) return BadRequest("Could not save changes to the database");

            //6. Map to DTO
            var newRating = _mapper.Map<FlaggingDto>(rating);

            //6. Publish event
            await _publishEndpoint.Publish(_mapper.Map<RatingCreated>(newRating));

            //7. Return created response
            return CreatedAtAction(nameof(GetFlaggedEstablishment),
                new
                {
                    estId = newRating.EstablishmentId,
                    flaggedBy = newRating.FlaggedBy,
                    flagId = newRating.FlagId,
                    flaggedOn = newRating.FlaggedOn
                },
                newRating);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    private async Task<Rating> PrepareRequest(CreateFlagDto request)
    {
        // 1. Fetch related entities
        var flag = await _flagRepository.GetFlagById(request.FlagId);
        if (flag is null) throw new ArgumentException("Invalid FlagId");
        var establishment = await _establishmentRepository.GetEstablishmentById(request.EstablishmentId);
        if (establishment is null) throw new ArgumentException("Invalid EstablishmentId");
        var user = await _usersRepository.GetUserById(request.FlaggedBy);
        if (user is null) throw new ArgumentException("Invalid FlaggerId");

        //2. Convert DateTime to UTC
        var flaggedOnUtc = Helper.ConvertToUtc(DateTime.Now);
        var modifiedOnUtc = request.ModifiedOn.HasValue ? Helper.ConvertToUtc(request.ModifiedOn.Value) : (DateTime?)null;

        //3. Map to entity
        var rating = _mapper.Map<Rating>(request);
        rating.FlaggedOn = flaggedOnUtc;
        rating.ModifiedOn = modifiedOnUtc;
        rating.Flag = flag;
        rating.Establishment = establishment;
        rating.User = user;

        return rating;
    }
}
