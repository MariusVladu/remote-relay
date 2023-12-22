namespace HomeAssistant.Domain.Entities;

public class Relay
{
    public required string Id { get; set; }
    public required List<int> Status { get; set; }
}
