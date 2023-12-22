using MQTTnet.Server;
using MQTTnet;
using HomeAssistant.MQTT;
using HomeAssistant.DI;
using Microsoft.Extensions.DependencyInjection;
using HomeAssistant.BusinessLogic.Contracts;

await StartMqttServer();
StartListenerInBackground();

var exitEvent = new ManualResetEvent(false);
exitEvent.WaitOne();


async Task StartMqttServer()
{
    var mqttFactory = new MqttFactory();

    var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

    using var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
    await mqttServer.StartAsync();

    Console.WriteLine("Started MQTT server");
}

void StartListenerInBackground()
{
    var serviceProvider = DependencyResolver.GetServiceProvider();
    var listener = new Listener(serviceProvider.GetRequiredService<IMqttMessageHandler>());

    new Task(async () => await listener.ContinuouslyListenToAllMessages()).Start();
}