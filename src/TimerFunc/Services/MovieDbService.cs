using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

public class MovieDbService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly string _apiKey = "Bearer " + Environment.GetEnvironmentVariable("MOVIE_DB_API_KEY");

    public MovieDbService(HttpClient httpClient, 
        ILoggerFactory loggerFactory)
    {
        _httpClient = httpClient;
        _logger = loggerFactory.CreateLogger<MovieDbService>();

        _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");

    }

    /// <summary>
    /// Retrieves the people changes from a specific date.
    /// </summary>
    /// <param name="date">The date to retrieve the changes from.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<Dictionary<int, PersonChangeUpdate>> GetPeopleChanges(DateTime date)
    {
        // Date format is yyyy-MM-dd for start and end date query parameters
        var startDate = date.AddDays(-1).ToString("yyyy-MM-dd");
        var end = date.ToString("yyyy-MM-dd");

        // pagingation - needs to start at 1
        int page = 1; 
        // total pages to query - will be updated from first call
        int totalPages = 1;
        // list of person change records!
        List<PersonChange> changes = [];

        // recursively get all pages of changes for the date
        do{  
            var request = new HttpRequestMessage(HttpMethod.Get, $"person/changes?end_date={end}&page={page}&start_date={startDate}");
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<PersonChangeResponse>(stringContent);
                changes.AddRange(content.results.Where(p => p.adult == true));
                totalPages = content.total_pages;
            }
            page++;
        }while(page <= totalPages);

        _logger.LogInformation($"GetPeopleChanges: found total of {changes.Count} changes");
        if(changes.Count > 0)
        {
            var res = await QueryPersonsChangedUpdates(date, changes);
            return res;
        }
        return [];
    }

    /// <summary>
    /// Queries the changed updates for persons based on the specified date and list of changes.
    /// </summary>
    /// <param name="date">The date to query for changed updates.</param>
    /// <param name="changes">The list of person changes.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task<Dictionary<int, PersonChangeUpdate>> QueryPersonsChangedUpdates(DateTime date, List<PersonChange> changes)
    {
        _logger.LogInformation("QueryPersonsChangedUpdates");

        Dictionary<int, PersonChangeUpdate> updates = [];

        foreach(var change in changes)
        {
            try{
                var request = new HttpRequestMessage(HttpMethod.Get, $"person/{change.id}/changes?page=1");
                request.Headers.Add("Authorization", _apiKey);

                var response = await _httpClient.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<PersonChangeUpdate>(stringContent);
                    updates.Add(change.id, content);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error getting changes for person {change.id} - {ex.Message}");
            }
        }
        return updates;
    }
}