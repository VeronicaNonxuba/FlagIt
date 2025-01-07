using MassTransit;
using Contracts;
using AutoMapper;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class RatingDeletedConsumer : IConsumer<RatingUpdated>
{
    private readonly IMapper _mapper;
    public RatingDeletedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<RatingUpdated> context)
    {
        Console.WriteLine("--> Consuming rating deleted for establishment with Id: " + context.Message.EstablishmentId);

        var rating = _mapper.Map<Rating>(context.Message);

        var result = await DB.Update<Rating>()
            .Match(r => r.ID == rating.ID)
            .Modify(r => r.IsDeleted, true)
            .ExecuteAsync();

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(RatingUpdated), "Rating deletion failed");
        }
    }
}
