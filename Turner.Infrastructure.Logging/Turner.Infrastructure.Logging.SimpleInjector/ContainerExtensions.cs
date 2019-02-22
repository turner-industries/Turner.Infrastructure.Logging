using System.Collections.Generic;
using SimpleInjector;
using Turner.Infrastructure.Logging.Decorators;
using Turner.Infrastructure.Mediator;

namespace Turner.Infrastructure.Logging.Mediator.SimpleInjector
{
    public static class SimpleInjectorLoggerConfiguration
    {
        public static void ConfigureLogging(Container container, IEnumerable<ILogger> loggers)
        {
            container.Register<ILogger>(() => new LogManager(loggers), Lifestyle.Singleton);

            container.RegisterDecorator(
                typeof(IRequestHandler<>),
                typeof(LoggingHandler<>),
                Lifestyle.Transient,
                x => !TypeUtility.ContainsAttribute(x.ImplementationType, typeof(DoNotLog)));

            container.RegisterDecorator(
                typeof(IRequestHandler<,>),
                typeof(LoggingHandler<,>),
                Lifestyle.Transient,
                x => !TypeUtility.ContainsAttribute(x.ImplementationType, typeof(DoNotLog)));
        }
    }
}
