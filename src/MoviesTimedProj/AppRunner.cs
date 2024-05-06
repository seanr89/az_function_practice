

using Microsoft.Extensions.Logging;

public class AppRunner
{
    internal readonly MovieDbService _movieDbService;
    internal readonly DataService _dataService;
    internal readonly PersonUpdater _updater;
    private readonly ILogger _logger;

    public AppRunner(MovieDbService movieDbService, PersonUpdater updater, DataService dataService,
        ILoggerFactory loggerFactory){
        _logger = loggerFactory.CreateLogger<AppRunner>();
        _movieDbService = movieDbService;
        _dataService = dataService;
        _updater = updater;
    }

    public void RunUpdate()
    {

    }
}