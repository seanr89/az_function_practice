using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

public class MovieDbService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly string _remoteServiceBaseUrl;

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
        request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmMzdhZWExMmEzZTJiMTE0NzdlMjcyMjgyNjc4YWIwYSIsInN1YiI6IjY2MTZkOTFjMGYxZTU4MDE3ZDQ2YWYxNSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.K6SGjNgjrbn_PDVV18p5Ay1XRlP73gq2Xzu1WFft80I");
    
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
}