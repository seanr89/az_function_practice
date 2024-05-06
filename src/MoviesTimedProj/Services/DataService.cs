

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DataService
{
    private readonly ILogger _logger;
    private readonly AppDbContext _appDbContext;
    public DataService(ILoggerFactory loggerFactory,
        AppDbContext appDbContext)
    {
        _logger = loggerFactory.CreateLogger<DataService>();
        _appDbContext = appDbContext;
    }

    public async Task<Movie> GetMovie(int id)
    {
        return await _appDbContext.Movies.FindAsync(id);
    }

    public async Task<List<Movie>> GetMovies(List<int> ids)
    {
        return await _appDbContext.Movies.Where(p => ids.Contains(p.id)).ToListAsync();
    }

    /// <summary>
    /// Updates a Movie in the database.
    /// </summary>
    /// <param name="rec">The rec object to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateMovie(Movie rec)
    {
        _appDbContext.Movies.Update(rec);
        await _appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Updates a list of records in the database.
    /// </summary>
    /// <param name="movies">The list of movies to update.</param>
    /// <returns>The number of entities updated in the database.</returns>
    public async Task<int> UpdateMovies(List<Movie> movies)
    {
        _appDbContext.Movies.UpdateRange(movies);
        return await _appDbContext.SaveChangesAsync();
    }
}