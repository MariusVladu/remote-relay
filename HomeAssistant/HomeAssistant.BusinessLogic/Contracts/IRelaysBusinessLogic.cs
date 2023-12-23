using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic.Contracts;

public interface IRelaysBusinessLogic
{
    Task<List<Relay>> GetAll();
    Task SwitchOn(string relayId, int k);
    Task SwitchOff(string relayId, int k);
}
