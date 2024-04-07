

using Microsoft.Extensions.Logging;

public class DataService
{
    private readonly ILogger _logger;
    public DataService(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<DataService>();
    }

    public void GetData()
    {
        // Get data from a database or API
        Thread.Sleep(1000);
        _logger.LogInformation("Data retrieved successfully");
    }
}