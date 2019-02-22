using System;

namespace Turner.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string log, params object[] data);
    }
}
