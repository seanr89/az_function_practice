using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

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
    public async Task GetPeopleChanges(DateTime date)
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

        // recursively get all pages of changes
        do{
            _logger.LogInformation($"Getting people changes for page {page}");    
            var request = new HttpRequestMessage(HttpMethod.Get, $"person/changes?end_date={end}&page={page}&start_date={startDate}");
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PersonChangeResponse>();
                changes.AddRange(content.results.Where(p => p.adult == true));
                totalPages = content.total_pages;
            }
            page++;
        }while(page <= totalPages);

        _logger.LogInformation($"GetPeopleChanges: {changes.Count} changes");
        if(changes.Count > 0)
        {
            await QueryPersonsChangedUpdates(date, changes);
        }
    }

    /// <summary>
    /// Queries the changed updates for persons based on the specified date and list of changes.
    /// </summary>
    /// <param name="date">The date to query for changed updates.</param>
    /// <param name="changes">The list of person changes.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task QueryPersonsChangedUpdates(DateTime date, List<PersonChange> changes)
    {
        _logger.LogInformation("QueryPersonsChangedUpdates");

        foreach(var change in changes)
        {
            //_logger.LogInformation($"Person {change.id} has changed");
            var request = new HttpRequestMessage(HttpMethod.Get, $"person/{change.id}/changes?page=1");
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PersonChangeUpdate>();
                _logger.LogInformation($"Person {change.id} data found: {content.changes.Count} changes");
            }
        }
    }

    /// <summary>
    /// Test Authentication.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task TestAuthentication()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "authentication");
        request.Headers.Add("Authorization", _apiKey);
    
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Authenticated successfully");
            return;
        }
        _logger.LogWarning("Failed to authenticate");
    }
}