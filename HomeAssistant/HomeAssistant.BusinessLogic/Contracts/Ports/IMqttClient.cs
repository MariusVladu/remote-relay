namespace HomeAssistant.BusinessLogic.Contracts.Ports;

public interface IMqttClient
{
    Task Publish<T>(string topic, T payload);
}
