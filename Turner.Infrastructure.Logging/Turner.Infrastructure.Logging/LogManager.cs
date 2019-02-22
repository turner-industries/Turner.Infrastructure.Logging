using System.Collections.Generic;

namespace Turner.Infrastructure.Logging
{
    public class LogManager : ILogManager
    {
        private readonly List<ILogger> _loggers;

        public LogManager(IEnumerable<ILogger> loggers)
        {
            _loggers = new List<ILogger>(loggers);
        }

        public void Info(string log, params object[] data)
        {
            _loggers.ForEach(x => x.Info(log, data));
        }
    }
}
