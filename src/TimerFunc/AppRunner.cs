
public class AppRunner
{
    internal readonly MovieDbService _movieDbService;
    internal readonly DataService _dataService;
    internal readonly PersonUpdater _updater;

    public AppRunner(MovieDbService movieDbService, PersonUpdater updater, DataService dataService){
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

            foreach(var update in updates)
            {
                Console.WriteLine($"Person {update.Key}");
                var person = await _dataService.GetPerson(update.Key);
                if(person is not null)
                {
                    Console.WriteLine($"Person {person.name} found");
                    _updater.TryUpdatePersonWithChanges(person, update.Value.changes);
                }
                else
                {
                    Console.WriteLine($"Person {update.Key} not found");
                }
            }
        }).Wait();
    }
}