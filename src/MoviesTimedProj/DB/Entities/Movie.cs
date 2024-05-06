
using System.ComponentModel.DataAnnotations.Schema;

[Table("Movie", Schema = "movies")]
public class Movie
{
    public int id { get; set; }
    public string title { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}