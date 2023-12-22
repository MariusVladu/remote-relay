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
            var switchesStatuses = message.Payload["status"].Deserialize<List<bool>>()!;

            var relay = await relaysService.TryGetById(relayId);
            if (relay is null)
            {
                relay = new Relay
                {
                    Id = relayId,
                    Switches = switchesStatuses
                        .Select((status, index) => new Switch
                        {
                            K = index,
                            State = false,
                            Label = index.ToString()
                        })
                        .ToList()
                };
            }

            for (int i = 0; i < switchesStatuses.Count; i++)
            {
                relay.Switches[i].State = switchesStatuses[i];
            }

            await relaysService.Upsert(relay);
        }
    }
}
