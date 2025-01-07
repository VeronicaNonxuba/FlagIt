using Contracts;
using MassTransit;

namespace FlaggingService.Consumers;

public class RatingCreatedFaultConsumer : IConsumer<Fault<RatingCreated>>
{
    public async Task Consume(ConsumeContext<Fault<RatingCreated>> context)
    {
        Console.WriteLine($"--> Consuming faulty creation message");

        var exception = context.Message.Exceptions.First();

        //NB - for later: Log the exception, send an email, etc.
        //just for now, we'll cater for dummy exception and print to console
        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Color = "Yellow";
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine($"--> Exception: {exception.Message}");
        }
    }
}
