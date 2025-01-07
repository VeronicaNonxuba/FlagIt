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
        try
        {
            return Ok(await _ratingRepository.GetRatings(DateTime.Now.ToString()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{request}")]
    public async Task<ActionResult<FlaggingDto>> GetRating([FromBody] RequestItem request)
    {
        try
        {
            return Ok(await _ratingRepository.GetFlaggingDtoById(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> FlagAnEstablishment([FromBody] CreateFlagDto request)
    {
        try
        {
            //1. Prepare request
            var rating = await PrepareRequest(request);

            //2. Map to DTO
            var newRating = _mapper.Map<FlaggingDto>(rating);

            //3. Publish event
            await _publishEndpoint.Publish(_mapper.Map<RatingCreated>(newRating));

            //4. Save to database
            var response = await _ratingRepository.FlagAnEstablishment(rating) > 0;

            //5. Return error if save failed
            if (!response) return BadRequest("Could not save changes to the database");

            #region Commented out - having issues with the CreatedAtAction method
            // //7. Return created response
            // return CreatedAtAction(nameof(GetFlaggedEstablishment),
            //     new
            //     {
            //         estId = newRating.EstablishmentId,
            //         flaggedBy = newRating.FlaggedBy,
            //         flagId = newRating.FlagId,
            //         flaggedOn = newRating.FlaggedOn
            //     },
            //     newRating);
            #endregion

            return Ok(newRating.EstablishmentId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteFlaggingEntry([FromBody] RequestItem requestObj)
    {
        try
        {
            requestObj.FlaggedOn = Helper.ConvertToUtc(requestObj.FlaggedOn);

            var newRating = await _ratingRepository.GetFlaggingDtoById(requestObj) ??
                            throw new ArgumentException("Invalid Flagging Entry");


            await _publishEndpoint.Publish(_mapper.Map<RatingUpdated>(newRating));

            await _ratingRepository.DeleteRatingEntry(requestObj);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    private async Task<Rating> PrepareRequest(CreateFlagDto request)
    {
        try
        {
            // 1. Fetch related entities
            var flag = await _flagRepository.GetFlagById(request.FlagId) ??
                        throw new ArgumentException("Invalid FlagId");
            var establishment = await _establishmentRepository.GetEstablishmentById(request.EstablishmentId) ??
                        throw new ArgumentException("Invalid EstablishmentId");
            var user = await _usersRepository.GetUserById(request.FlaggedBy) ??
                        throw new ArgumentException("Invalid FlaggerId");

            //2. Convert DateTime to UTC
            var flaggedOnUtc = Helper.ConvertToUtc(DateTime.Now);
            var modifiedOnUtc = request.ModifiedOn.HasValue ?
                                Helper.ConvertToUtc(request.ModifiedOn.Value)
                                : (DateTime?)null;

            //3. Map to entity
            var rating = _mapper.Map<Rating>(request);
            rating.FlaggedOn = flaggedOnUtc;
            rating.ModifiedOn = modifiedOnUtc;
            rating.Flag = flag;
            rating.Establishment = establishment;
            rating.User = user;

            return rating;
        }
        catch (Exception ex)
        {
            throw new Exception("Error preparing request: ", ex);
        }
    }
}
