

using Microsoft.Extensions.Logging;

public class DataService
{
    private readonly ILogger _logger;
    private readonly MovieDbService _movieDbService;
    public DataService(ILoggerFactory loggerFactory,
        MovieDbService movieDbService)
    {
        _logger = loggerFactory.CreateLogger<DataService>();
        _movieDbService = movieDbService;
    }

    public void GetData()
    {
        // Get data from a database or API
        //Thread.Sleep(100);
        _logger.LogInformation($"Data retrieved successfully with env var: " +
            $"{Environment.GetEnvironmentVariable("ENV_VAR", EnvironmentVariableTarget.Process) ?? "Not set"}");

        //_movieDbService.Authenticate().Wait();

        DateTime date = DateTime.Now;
        _movieDbService.GetPeopleChanges(date).Wait();
    }
}