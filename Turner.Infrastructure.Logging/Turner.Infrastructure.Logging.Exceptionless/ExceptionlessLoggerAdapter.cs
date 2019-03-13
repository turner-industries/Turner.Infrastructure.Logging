using Exceptionless;
using Newtonsoft.Json;
using Turner.Infrastructure.Logging.Decorators;

namespace Turner.Infrastructure.Logging.Exceptionless
{
    public class ExceptionlessLoggerAdapter : ILogger
    {
        public void Info(string log, params object[] data)
        {
            var exceptionlessBuilder = ExceptionlessClient.Default.CreateLog(log);

            foreach (var @object in data)
            {
                var serialized = JsonConvert.SerializeObject(@object, Formatting.None, new SsnMaskJsonConverter());

                exceptionlessBuilder.AddObject(serialized);
            }

            exceptionlessBuilder.Submit();
        }
    }
}
