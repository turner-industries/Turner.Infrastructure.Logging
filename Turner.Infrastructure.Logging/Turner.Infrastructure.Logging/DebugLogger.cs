using System;
using Newtonsoft.Json;
using Turner.Infrastructure.Logging.Decorators;

namespace Turner.Infrastructure.Logging
{
    public class DebugLogger : ILogger
    {
        private readonly SsnMaskJsonConverter _ssnMaskJsonConverter;

        public DebugLogger()
        {
            _ssnMaskJsonConverter = new SsnMaskJsonConverter();
        }
        public void Info(string log, params object[] data)
        {
            System.Diagnostics.Debug.WriteLine(log);

            foreach (var @object in data)
            {
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(@object, Formatting.Indented, _ssnMaskJsonConverter));
            }
        }
    }
}
