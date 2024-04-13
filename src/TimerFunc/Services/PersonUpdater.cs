

public class PersonUpdater
{
    internal readonly DataService _dataService;
    public PersonUpdater(DataService dataService)
    {
        _dataService = dataService;
    }

    public void TryUpdatePersonWithChanges(Person person, List<Change> changes)
    {
        foreach(var change in changes)
        {
            switch(change.key)
            {
                case "name":
                    person.name = change.items[0].value;
                    break;
                case "birthday":
                    person.date_of_birth = change.items[0].value;
                    break;
                case "deathday":
                    person.date_of_death = change.items[0].value;
                    break;
                case "imdb_id":
                    person.imdb_id = change.items[0].value;
                    break;
                case "place_of_birth":
                    person.place_of_birth = change.items[0].value;
                    break;
                case "known_for_department":
                    person.known_for_department = change.items[0].value;
                    break;
                case "biography":
                    person.biography = change.items[0].value;
                    break;
            }
            person.updated_at = DateTime.Now;
        }

        //TODO: return here?
        //_dataService.UpdatePerson(person);
    }
}