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
}