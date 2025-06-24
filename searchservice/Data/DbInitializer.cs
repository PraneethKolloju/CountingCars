using System.Text.Json;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Entities;

namespace searchservice.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication application)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings
                .FromConnectionString(application.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

            var count = await DB.CountAsync<Item>();

            if (count == 0)
            {
                var itemData = await File.ReadAllTextAsync("Data/auctions.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

                await DB.SaveAsync(items);

            }


        }
    }
}