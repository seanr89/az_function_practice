using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

public class MovieDbService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly string _apiKey = "Bearer myapikey";

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"person/changes?end_date=2024-04-10&page=1&start_date=2024-04-09");
        request.Headers.Add("Authorization", _apiKey);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(content);
        }
        else
        {
            Console.WriteLine("Failed to get people changes");
        }
    }
}