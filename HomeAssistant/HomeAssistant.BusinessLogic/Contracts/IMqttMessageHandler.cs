using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic.Contracts;

public interface IMqttMessageHandler
{
    Task HandleMessage(MqttMessage message);
}
