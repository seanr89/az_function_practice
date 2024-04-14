

using Microsoft.Extensions.Logging;

public class PersonUpdater
{
    internal readonly ILogger _logger;
    public PersonUpdater(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<PersonUpdater>();
    }

    /// <summary>
    /// Tries to update a person object with the given changes.
    /// </summary>
    /// <param name="person">The person object to update.</param>
    /// <param name="changes">The list of changes to apply.</param>
    public (bool updated, Person person) TryUpdatePersonWithChanges(Person person, List<Change> changes)
    {
        bool updating = false;
        foreach(var change in changes)
        {
            if(change.items is null || change.items[0].value is null)
            {
                _logger.LogWarning($"No items found for change {change.key}");
                continue;
            }
            switch(change.key)
            {
                case "name":
                    person.name = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "birthday":
                    person.date_of_birth = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "deathday":
                    person.date_of_death = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "imdb_id":
                    person.imdb_id = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "place_of_birth":
                    person.place_of_birth = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "known_for_department":
                    person.known_for_department = change.items[0].value.ToString();
                    updating = true;
                    break;
                case "biography":
                    person.biography = change.items[0].value.ToString();
                    updating = true;
                    break;
            }

            if(updating)
                person.updated_at = DateTime.Now;
        }
        return (updating, person);
        
    }
}