
using Microsoft.Extensions.Logging;

public class AppRunner
{
    internal readonly MovieDbService _movieDbService;
    internal readonly DataService _dataService;
    internal readonly PersonUpdater _updater;
    private readonly ILogger _logger;

    public AppRunner(MovieDbService movieDbService, PersonUpdater updater, DataService dataService,
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
        //var updates = _movieDbService.GetPeopleChanges(date).Result;

        Task.Run(async () => {
            var updates = await _movieDbService.GetPeopleChanges(date);

            int notFound = 0;
            foreach(var update in updates)
            {
                var person = await _dataService.GetPerson(update.Key);
                if(person is not null)
                {
                    _logger.LogInformation($"Person {person.name} found - Updating with changes");
                    _updater.TryUpdatePersonWithChanges(person, update.Value.changes);
                }
                else{
                    notFound++;
                }
            }
            _logger.LogWarning($"Total of {notFound} people not found in database");
        }).Wait();
    }
}