using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic;

internal class RelaysBusinessLogic(IRelaysService relaysService) : IRelaysBusinessLogic
{
    public Task<List<Relay>> GetAll()
    {
        return relaysService.GetAll();
    }
}
