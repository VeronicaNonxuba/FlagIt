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
            .Key(x => x.FlaggedBy, KeyType.Text)
            .CreateAsync();
    }
}
