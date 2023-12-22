using MQTTnet.Server;
using MQTTnet;
using HomeAssistant.MQTT;

var mqttFactory = new MqttFactory();

var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

using var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
await mqttServer.StartAsync();

new Task(async () => await Listener.StartListeningToAllMessages()).Start();

Console.WriteLine("Started MQTT server");

var exitEvent = new ManualResetEvent(false);
exitEvent.WaitOne();
