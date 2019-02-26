using Moq;
using NUnit.Framework;
using Turner.Infrastructure.Mediator;

namespace Turner.Infrastructure.Logging.Mediator.Tests
{
    [TestFixture]
    public class LoggingHandlerTests
    {
        [Test]
        public void HandleAsync_NonGenericResponse_CallsInnerHandleAsync()
        {
            var loggerMock = new Mock<ILogger>();
            var handlerMock = new Mock<IRequestHandler<TestRequest>>();
            var loggingHandler = new LoggingHandler<TestRequest>(loggerMock.Object, handlerMock.Object);
            var request = new TestRequest();

            loggingHandler.HandleAsync(request);

            handlerMock.Verify(x => x.HandleAsync(request), Times.Once);
        }

        [Test]
        public void HandleAsync_WithGenericResponse_CallsInnerHandleAsync()
        {
            var loggerMock = new Mock<ILogger>();
            var handlerMock = new Mock<IRequestHandler<TestRequestWithResponse, TestResponse>>();
            var loggingHandler = new LoggingHandler<TestRequestWithResponse, TestResponse>(loggerMock.Object, handlerMock.Object);
            var request = new TestRequestWithResponse();

            loggingHandler.HandleAsync(request);

            handlerMock.Verify(x => x.HandleAsync(request), Times.Once);
        }
    }

    public class TestRequest : IRequest
    {
    }

    public class TestRequestWithResponse : IRequest<TestResponse>
    {
    }

    public class TestResponse { }
}
