
/// <summary>
/// Update update from individual changes for a single person!
/// </summary>
/// <value></value>
public record MovieChangeUpdate
{
    public List<Change> changes { get; set; }
}

public record Change
{
    public string key { get; set; }
    public List<Item>? items { get; set; }
}

public record Item
{
    public string action { get; set; }
    public object? value { get; set; }
}