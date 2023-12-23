using MQTTnet.Client;
using MQTTnet;
using System.Text.Json;
using HomeAssistant.Domain.Entities;
using System.Text.Json.Nodes;
using HomeAssistant.BusinessLogic.Contracts;
using HomeAssistant.DI;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.MQTT;

public static class Listener
{
    private static IServiceProvider RootServiceProvider = DependencyResolver.GetServiceProvider();

    public static async Task ContinuouslyListenToAllMessages()
    {
        var mqttFactory = new MqttFactory();
        var mqttClient = mqttFactory.CreateMqttClient();

        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("0.0.0.0").Build();
        mqttClient.ApplicationMessageReceivedAsync += HandleMessage;

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter("#")
            .Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        Console.WriteLine("Subscribed to all topics");

        var exitEvent = new ManualResetEvent(false);
        exitEvent.WaitOne();
    }

    private static async Task HandleMessage(MqttApplicationMessageReceivedEventArgs e)
    {
        var topic = e.ApplicationMessage.Topic;
        if (topic.StartsWith("command"))
            return;
        
        var rawPayload = e.ApplicationMessage.ConvertPayloadToString();

        try
        {
            var payload = JsonSerializer.Deserialize<JsonNode>(rawPayload);
            ArgumentNullException.ThrowIfNull(payload);

            var clientId = payload["client_id"]!.ToString();

            var mqttMessage = new MqttMessage(clientId, topic, payload);

            var scope = RootServiceProvider.CreateScope();
            var messagesHandler = scope.ServiceProvider.GetRequiredService<IMqttMessageHandler>();

            await messagesHandler.HandleMessage(mqttMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to handle message for topic {topic}. Raw payload: {rawPayload}. Exception: {ex}");
        }
    }
}
