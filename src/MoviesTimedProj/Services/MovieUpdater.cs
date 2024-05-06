using Microsoft.Extensions.Logging;

public class MovieUpdater
{
    internal readonly ILogger _logger;
    public MovieUpdater(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MovieUpdater>();
    }

    /// <summary>
    /// Tries to update a movie object with the given changes.
    /// Limited by the changes that can be applied to an object.
    /// </summary>
    /// <param name="person">The movie object to update.</param>
    /// <param name="changes">The list of changes to apply.</param>
    /// <returns>A tuple with a boolean indicating if the movie was updated and the updated person object.</returns>
    public (bool updated, Movie rec) TryUpdateMovieWithChanges(Movie movie, List<Change> changes)
    {
        bool updating = false;
        foreach(var change in changes)
        {
            if(change.items is null || change.items[0].value is null)
            {
                _logger.LogWarning($"No items found for change {change.key}");
                continue;
            }
            switch(change.key){
                case "title":
                    movie.title = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "original_title":
                    movie.original_title = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "overview":
                    movie.overview = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "tagline":
                    movie.tagline = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "keywords":
                    movie.keywords = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "status":
                    movie.status = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "poster_url":
                    movie.poster_url = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "homepage":                
                    movie.homepage = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "languages":
                    movie.languages = change.items[0].value.ToString();
                    updating = true;
                    break;
            }

            if(updating)
                movie.updated_at = DateTime.Now;
        }
        return (updating, movie);
    }
}