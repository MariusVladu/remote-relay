using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic.Contracts.Ports;

public interface IRelaysService
{
    Task<List<Relay>> GetAll();
    Task Upsert(Relay relay);
}
