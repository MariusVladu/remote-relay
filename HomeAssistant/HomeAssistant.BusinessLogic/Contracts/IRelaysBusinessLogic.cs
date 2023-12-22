using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic.Contracts;

public interface IRelaysBusinessLogic
{
    Task<List<Relay>> GetAll();
}
