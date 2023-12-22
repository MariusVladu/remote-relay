using System.Text.Json.Nodes;

namespace HomeAssistant.Domain.Entities;

public record MqttMessage
{
    public required string ClientId { get; init; }
    public required JsonNode Payload { get; init; }
}
