

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
        try{
            // Get the current date for usage on queries
            DateTime date = DateTime.Now;
            _movieDbService.GetPeopleChanges(date).Wait();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred");
        }

    }
}