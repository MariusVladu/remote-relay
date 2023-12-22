using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.Domain.Entities;
using MongoDB.Driver;

namespace HomeAssistant.Services;

internal class RelaysService(DataContext dataContext) : IRelaysService
{
    public async Task<List<Relay>> GetAll()
    {
        return await dataContext.Relays.Find(_ => true).ToListAsync();
    }

    public async Task<Relay?> TryGetById(string id)
    {
        return await dataContext.Relays.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    private static ReplaceOptions UpsertReplaceOptions = new ReplaceOptions { IsUpsert = true };
    public async Task Upsert(Relay relay)
    {
        await dataContext.Relays.ReplaceOneAsync(r => r.Id == relay.Id, relay, UpsertReplaceOptions);
    }
}
