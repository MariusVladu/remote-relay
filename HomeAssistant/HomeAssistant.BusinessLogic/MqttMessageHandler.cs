using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic;

public class MqttMessageHandler : IMqttMessageHandler
{
    public Task HandleMessage(MqttMessage message)
    {
        throw new NotImplementedException();
    }
}
