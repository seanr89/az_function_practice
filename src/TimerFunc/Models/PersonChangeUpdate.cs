
public record PersonChangeUpdate
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