using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using babelhealth.Logging;

namespace babelhealth.Controllers
{
    /// <summary>
    /// Base controller for API controllers
    /// </summary>
    public class BaseController : ControllerBase
    {
        private LogHelper _logHelper;

        /// <summary>
        /// Constructor with logger factory
        /// </summary>
        /// <param name="loggerFactory">DI logger factory</param>
        public BaseController(ILoggerFactory loggerFactory)
        {
            _logHelper = new LogHelper(loggerFactory, this.GetType().ToString());
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="message"></param>
        protected void Log(string message)
        {
            _logHelper.Log(message);
        }
    }
}
