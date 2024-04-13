using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerFunc
{
    public class TimerFunc
    {
        private readonly ILogger _logger;
        private readonly AppRunner _runner;

        public TimerFunc(ILoggerFactory loggerFactory, AppRunner runner)
        {
            _logger = loggerFactory.CreateLogger<TimerFunc>();
            _runner = runner;
        }

        [Function("TimerFunc")]
        public void Run([TimerTrigger("1 12 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _runner.RunUpdate();
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
