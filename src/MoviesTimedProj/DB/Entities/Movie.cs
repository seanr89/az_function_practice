
using System.ComponentModel.DataAnnotations.Schema;

[Table("Movie", Schema = "movies")]
public class Movie
{
    public int id { get; set; }
    public string title { get; set; }
    public string original_title { get; set; }
    public string overview { get; set; }
    public string tagline { get; set; }
    public string keywords { get; set; }
    public string status { get; set; }
    public string poster_url { get; set; }
    public string homepage { get; set; }
    public string languages { get; set; }
    public int revenue { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}