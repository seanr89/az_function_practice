

using Microsoft.Extensions.Logging;

public class AppRunner
{
    internal readonly MovieDbService _movieDbService;
    internal readonly DataService _dataService;
    internal readonly MovieUpdater _updater;
    private readonly ILogger _logger;

    public AppRunner(MovieDbService movieDbService, MovieUpdater updater, DataService dataService,
        ILoggerFactory loggerFactory){
        _logger = loggerFactory.CreateLogger<AppRunner>();
        _movieDbService = movieDbService;
        _dataService = dataService;
        _updater = updater;
    }

    public void RunUpdate()
    {
        // Get the current date for usage on queries
        DateTime date = DateTime.Now;

        // Handle async processess
        Task.Run(async () => {
            // run job to talk to movie db api
            var updates = await _movieDbService.GetChanges(date);

            int notFound = 0;
            List<Movie> changes = new();
            var movies = await _dataService.GetMovies(updates.Keys.ToList());
            foreach(var update in updates)
            {
                var movie = movies.FirstOrDefault(p => p.id == update.Key);
                if(movie is not null)
                {
                    var res = _updater.TryUpdateMovieWithChanges(movie, update.Value.changes);
                    if(res.updated)
                    {
                        changes.Add(movie);
                    }
                }
                else{
                    notFound++;
                }
            }
            if(changes.Count > 0)
            {
                _logger.LogWarning($"Updating {changes.Count} records in database");
                //await _dataService.UpdatePersons(changes);
            }
            _logger.LogWarning($"Total of {notFound} records not found in database");
        }).Wait();
    }
}