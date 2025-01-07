using MassTransit;
using SearchService.Consumers;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<RatingCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<RatingDeletedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("search-rating-created", e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<RatingCreatedConsumer>(context);
            e.ConfigureConsumer<RatingDeletedConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDb(app).Wait();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
app.Run();
