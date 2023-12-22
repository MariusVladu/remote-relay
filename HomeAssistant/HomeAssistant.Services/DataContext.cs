using HomeAssistant.Domain.Entities;
using MongoDB.Driver;

namespace HomeAssistant.Services;

internal class DataContext
{
    public DataContext()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("HomeAssistant");

        Relays = database.GetCollection<Relay>("Relays");
    }

    public IMongoCollection<Relay> Relays { get; }
}
