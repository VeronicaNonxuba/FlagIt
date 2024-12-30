using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb",
        MongoClientSettings.FromConnectionString(
            app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Rating>()
            .Key(x => x.EstablishmentName, KeyType.Text)
            .Key(x => x.Username, KeyType.Text)
            .Key(x => x.EstablishmentTypeName, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Rating>();
        if (count == 0)
        {
            Console.WriteLine("Seeding database...");
            var itemData = await File.ReadAllTextAsync("Data/Ratings.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<List<Rating>>(itemData, options);
            if (items != null)
            {
                await DB.SaveAsync(items);
            }
            Console.WriteLine("Database seeded.");
        }
    }
}
