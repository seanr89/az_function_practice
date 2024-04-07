using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerFunc
{
    public class TimerFunc
    {
        private readonly ILogger _logger;
        private readonly DataService _dataService;

        public TimerFunc(ILoggerFactory loggerFactory, DataService dataService)
        {
            _logger = loggerFactory.CreateLogger<TimerFunc>();
            _dataService = dataService;
        }

        [Function("TimerFunc")]
        public void Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _dataService.GetData();
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
