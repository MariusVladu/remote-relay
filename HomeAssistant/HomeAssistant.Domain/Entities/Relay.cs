namespace HomeAssistant.Domain.Entities;

public class Relay
{
    public required string Id { get; set; }
    public required List<Switch> Switches { get; set; }
}

public class Switch
{
    public required int K { get; set; }
    public required bool State { get; set; }
    public required string Label { get; set; }
}
