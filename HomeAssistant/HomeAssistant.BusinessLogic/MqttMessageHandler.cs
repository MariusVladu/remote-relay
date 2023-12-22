using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.BusinessLogic.Contracts.Ports;
using HomeAssistant.Domain.Entities;
using System.Text.Json;

namespace HomeAssistant.BusinessLogic;

internal class MqttMessageHandler(IRelaysService relaysService) : IMqttMessageHandler
{
    public async Task HandleMessage(MqttMessage message)
    {
        if (message.Topic is "status")
            await HandleStatusMessage(message);
    }

    private async Task HandleStatusMessage(MqttMessage message)
    {
        if (message.Payload["type"]?.ToString() is "relay")
        {
            var relayId = message.ClientId;
            var relayStatus = message.Payload["status"].Deserialize<List<int>>()!;

            var relay = new Relay { Id = relayId, Status = relayStatus };

            await relaysService.Upsert(relay);
        }
    }
}
