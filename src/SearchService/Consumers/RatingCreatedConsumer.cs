using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class RatingCreatedConsumer : IConsumer<RatingCreated>
{
    private readonly IMapper _mapper;
    public RatingCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<RatingCreated> context)
    {
        Console.WriteLine("--> Consuming rating created: " + context.Message.EstablishmentId);

        var rating = _mapper.Map<Rating>(context.Message);

        await rating.SaveAsync();
    }
}
