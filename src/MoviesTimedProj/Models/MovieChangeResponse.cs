
/// <summary>
/// Change object objects for a batches person changes model
/// </summary>
/// <value></value>
public record MovieChangeResponse
{
    public List<MovieChange> results { get; set; }

    public int page { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}

public record MovieChange{
    public int id { get; set; }
    public bool? adult { get; set; }
}