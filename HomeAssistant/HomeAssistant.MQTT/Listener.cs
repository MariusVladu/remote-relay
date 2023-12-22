﻿using MQTTnet.Client;
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
        var clientId = e.ClientId;
        var topic = e.ApplicationMessage.Topic;
        var rawPayload = e.ApplicationMessage.ConvertPayloadToString();

        try
        {
            var payload = JsonSerializer.Deserialize<JsonNode>(rawPayload);
            ArgumentNullException.ThrowIfNull(payload);

            var mqttMessage = new MqttMessage(clientId, topic, payload);

            var scope = RootServiceProvider.CreateScope();
            var messagesHandler = scope.ServiceProvider.GetRequiredService<IMqttMessageHandler>();

            await messagesHandler.HandleMessage(mqttMessage);
        }
        catch (Exception)
        {
            Console.WriteLine($"Failed to deserialize payload for topic {topic}. Raw payload:\n{rawPayload}");
        }
    }
}
