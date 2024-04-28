using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MoviesTimedProj
{
    public class MoviesTimedProj
    {
        private readonly ILogger _logger;
        //private readonly AppRunner _runner;

        public MoviesTimedProj(ILoggerFactory loggerFactory, AppRunner runner)
        {
            _logger = loggerFactory.CreateLogger<MoviesTimedProj>();
            _runner = runner;
        }

        [Function("MoviesTimedProj")]
        public void Run([TimerTrigger("0 0 22 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            _logger.LogInformation("Timer function finished");
        }
    }
}
