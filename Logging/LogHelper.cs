using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace babelhealth.Logging
{
    /// <summary>
    /// Helper class for application logging
    /// </summary>
    public class LogHelper
    {
        private ILogger _logger;
        private Stopwatch _stopWatch;
        private long _lastLogElapsedTime = 0L;

        public LogHelper(ILoggerFactory loggerFactory, string name)
        {
            _logger = loggerFactory.CreateLogger(name);
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            var elapsedTotal = _stopWatch.ElapsedMilliseconds;
            var elapsedTime = string.Format(" elapsed [total: {0} ms | last log: {1} ms]", elapsedTotal, elapsedTotal - _lastLogElapsedTime);
            _logger.LogDebug(string.Format("{0}{1}", message, elapsedTime));
            _lastLogElapsedTime = _stopWatch.ElapsedMilliseconds;
        }
    }
}