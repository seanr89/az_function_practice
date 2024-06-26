
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

        // Handle async processess
        Task.Run(async () => {
            // run job to talk to movie db api
            var updates = await _movieDbService.GetPeopleChanges(date);

            int notFound = 0;
            List<Person> changes = new();
            var persons = await _dataService.GetPeople(updates.Keys.ToList());
            foreach(var update in updates)
            {
                var person = persons.FirstOrDefault(p => p.id == update.Key);
                if(person is not null)
                {
                    var res = _updater.TryUpdatePersonWithChanges(person, update.Value.changes);
                    if(res.updated)
                    {
                        changes.Add(person);
                    }
                }
                else{
                    notFound++;
                }
            }
            if(changes.Count > 0)
            {
                _logger.LogWarning($"Updating {changes.Count} people in database");
                //await _dataService.UpdatePersons(changes);
            }
            _logger.LogWarning($"Total of {notFound} people not found in database");
        }).Wait();
    }
}