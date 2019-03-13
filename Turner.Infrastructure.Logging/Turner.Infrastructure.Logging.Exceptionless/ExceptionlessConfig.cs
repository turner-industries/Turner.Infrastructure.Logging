using Exceptionless;
using Microsoft.AspNetCore.Builder;

namespace Turner.Infrastructure.Logging.Exceptionless
{
    public static class ExceptionlessConfig
    {
        public static void Configure(IApplicationBuilder app, ILoggingSettings loggingSettings)
        {
            app.UseExceptionless(loggingSettings.ExceptionlessApiKey);
            ExceptionlessClient.Default.Startup(loggingSettings.ExceptionlessApiKey);
        }
    }
}
