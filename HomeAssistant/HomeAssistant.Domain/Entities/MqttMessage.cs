using System.Text.Json.Nodes;

namespace HomeAssistant.Domain.Entities;

public record MqttMessage(string ClientId, string Topic, JsonNode Payload);
