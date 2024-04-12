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

        // using Microsoft.Net.Http.Headers;
        // The GitHub API requires two headers.
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");

    }

    public async Task Authenticate()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "authentication");
        request.Headers.Add("Authorization", _apiKey);
    
        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Authenticated successfully");
        }
        else
        {
            Console.WriteLine("Failed to authenticate");
        }
    }

    public async Task GetPeopleChanges(DateTime date)
    {
        var startDate = date.AddDays(-1).ToString("yyyy-MM-dd");
        var end = date.ToString("yyyy-MM-dd");
        int page = 1; // needs to start at 1
        int totalPages = 1;
        List<PersonChange> changes = [];

        do{
            _logger.LogInformation($"Getting people changes for page {page}");    
            var request = new HttpRequestMessage(HttpMethod.Get, $"person/changes?end_date={end}&page={page}&start_date={startDate}");
            request.Headers.Add("Authorization", _apiKey);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<PersonChangeResponse>();
                changes.AddRange(content.results);
                totalPages = content.total_pages;
            }
            page++;
        }while(page <= totalPages);

        _logger.LogInformation($"GetPeopleChanges:Got {changes.Count} changes");
    }
}