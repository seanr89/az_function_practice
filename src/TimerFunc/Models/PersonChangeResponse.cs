
public record PersonChangeResponse
{
    public List<PersonChange> changes { get; set; }

    public int page { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
}

public record PersonChange{
    public int id { get; set; }
    public bool adult { get; set; }
}