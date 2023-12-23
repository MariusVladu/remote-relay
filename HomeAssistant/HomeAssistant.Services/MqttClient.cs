using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;

namespace HomeAssistant.Services;

internal class MqttClient : BusinessLogic.Contracts.Ports.IMqttClient
{
    private MQTTnet.Client.IMqttClient? mqttClient;

    public async Task Publish<T>(string topic, T payload)
    {
        if (mqttClient == null)
            await InitializeMqttClient();

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(JsonSerializer.Serialize(payload))
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
            .Build();

        await mqttClient!.PublishAsync(applicationMessage);
    }

    private async Task InitializeMqttClient()
    {
        mqttClient = new MqttFactory().CreateMqttClient();
        var mqttServer = Environment.GetEnvironmentVariable("MQTT_SERVER");

        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(mqttServer)
            .Build();

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
    }
}
