using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.Domain.Entities;

namespace HomeAssistant.BusinessLogic;

internal class RelaysBusinessLogic(IRelaysService relaysService, IMqttClient mqttClient) : IRelaysBusinessLogic
{
    public Task<List<Relay>> GetAll()
    {
        return relaysService.GetAll();
    }

    public async Task SwitchOff(string relayId, int k)
    {
        await mqttClient.Publish(GetCommandTopic(relayId), new RelayCommand("off", k));
    }

    public async Task SwitchOn(string relayId, int k)
    {
        await mqttClient.Publish(GetCommandTopic(relayId), new RelayCommand("on", k));
    }

    private string GetCommandTopic(string relayId) => $"command_{relayId}";
}
