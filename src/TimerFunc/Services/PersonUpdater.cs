

using Microsoft.Extensions.Logging;

public class PersonUpdater
{
    internal readonly DataService _dataService;
    internal readonly ILogger _logger;
    public PersonUpdater(DataService dataService, ILoggerFactory loggerFactory)
    {
        _dataService = dataService;
        _logger = loggerFactory.CreateLogger<PersonUpdater>();
    }

    public void TryUpdatePersonWithChanges(Person person, List<Change> changes)
    {
        _logger.LogInformation($"Updating person {person.name} with {changes.Count} changes");
        foreach(var change in changes)
        {
            _logger.LogInformation($"Updating person {person.name} with change {change.key}");
            if(change.items is null || change.items[0].value is null)
            {
                _logger.LogWarning($"No items found for change {change.key}");
                continue;
            }
            switch(change.key)
            {
                case "name":
                    person.name = change.items[0].value.ToString();
                    break;
                case "birthday":
                    person.date_of_birth = change.items[0].value.ToString();
                    break;
                case "deathday":
                    person.date_of_death = change.items[0].value.ToString();
                    break;
                case "imdb_id":
                    person.imdb_id = change.items[0].value.ToString();
                    break;
                case "place_of_birth":
                    person.place_of_birth = change.items[0].value.ToString();
                    break;
                case "known_for_department":
                    person.known_for_department = change.items[0].value.ToString();
                    break;
                case "biography":
                    person.biography = change.items[0].value.ToString();
                    break;
            }
            person.updated_at = DateTime.Now;
        }
        //_dataService.UpdatePerson(person);
    }
}