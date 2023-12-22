using MQTTnet.Client;
using MQTTnet;
using System.Text.Json;

namespace HomeAssistant.MQTT;

public class Listener
{
    private static MqttFactory mqttFactory = new MqttFactory();

    public static async Task StartListeningToAllMessages()
    {
        using var mqttClient = mqttFactory.CreateMqttClient();

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
        var message = e.ApplicationMessage.ConvertPayloadToString();
        Console.WriteLine($"Received application message:\nclientId={clientId}\ntopic={topic}\nmessage={message}");
    }
}
