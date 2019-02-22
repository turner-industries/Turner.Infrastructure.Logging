using Moq;
using NUnit.Framework;

namespace Turner.Infrastructure.Logging.Tests
{
    [TestFixture]
    public class LogManagerTests
    {
        [Test]
        public void Info_NoIssues_CallsInfoOnAllLoggers()
        {
            const string log = "This is a test";
            const int dataInt = 1;
            const string dataString = "Test";
            var logger1 = new Mock<ILogger>();
            var logger2 = new Mock<ILogger>();
            var manager = new LogManager(new []{logger1.Object, logger2.Object});

            manager.Info(log, dataInt, dataString);

            logger1.Verify(x => x.Info(log, dataInt, dataString), Times.Once);
            logger2.Verify(x => x.Info(log, dataInt, dataString), Times.Once);
        }
    }
}
