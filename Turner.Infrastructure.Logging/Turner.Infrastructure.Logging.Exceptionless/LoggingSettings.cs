namespace Turner.Infrastructure.Logging.Exceptionless
{
    public partial interface ILoggingSettings
    {
        bool UseExceptionless { get; set; }
        string ExceptionlessApiKey { get; set; }
    }

    public partial class LoggingSettings : ILoggingSettings
    {
        public bool UseExceptionless { get; set; }
        public string ExceptionlessApiKey { get; set; }
    }
}
