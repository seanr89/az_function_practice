using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ShowsTimedProj
{
    public class ShowsTimedProj
    {
        private readonly ILogger _logger;
        //private readonly AppRunner _runner;

        public ShowsTimedProj(ILoggerFactory loggerFactory, AppRunner runner)
        {
            _logger = loggerFactory.CreateLogger<ShowsTimedProj>();
            _runner = runner;
        }

        [Function("ShowsTimedProj")]
        public void Run([TimerTrigger("0 0 21 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            _logger.LogInformation("Timer function finished");
        }
    }
}
