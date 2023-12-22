using MQTTnet.Server;
using MQTTnet;
using HomeAssistant.MQTT;

await StartMqttServer();
StartListenerInBackground();

var exitEvent = new ManualResetEvent(false);
exitEvent.WaitOne();


async Task StartMqttServer()
{
    var mqttFactory = new MqttFactory();

    var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

    var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
    await mqttServer.StartAsync();

    Console.WriteLine("Started MQTT server");
}

void StartListenerInBackground()
{
    new Task(async () => await Listener.ContinuouslyListenToAllMessages()).Start();
}