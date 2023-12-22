using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAssistant.Services;

internal class RelaysService(DataContext dataContext) : IRelaysService
{
    public async Task<List<Relay>> GetAll()
    {
        return await dataContext.Relays.ToListAsync();
    }

    public async Task Upsert(Relay relay)
    {
        dataContext.Update(relay);

        await dataContext.SaveChangesAsync();
    }
}
