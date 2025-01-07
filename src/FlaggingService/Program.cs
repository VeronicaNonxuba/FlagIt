using FlaggingService.Consumers;
using FlaggingService.Data;
using FlaggingService.Data.Establishments;
using FlaggingService.Data.Flags;
using FlaggingService.Data.Users;
using MassTransit;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<FlaggingDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IFlagRepository, FlagRepository>();
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

// Configure Logging
builder.Logging.ClearProviders(); // Clear any defaults
builder.Logging.AddConsole();    // Adds Console logging provider
builder.Logging.AddDebug();       // Adds Debug logging provider
//builder.Logging.AddFilter("logs/app.txt"); // Adds logging to a file
// builder.Logging.AddApplicationInsights(""); // Add Application Insights logging

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<FlaggingDbContext>(c =>
    {
        c.QueryDelay = TimeSpan.FromSeconds(10);
        c.UsePostgres();
        c.UseBusOutbox();
    });

    x.AddConsumersFromNamespaceContaining<RatingCreatedFaultConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ration", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
