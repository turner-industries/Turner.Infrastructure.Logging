using Moq;
using NUnit.Framework;
using Turner.Infrastructure.Mediator;

namespace Turner.Infrastructure.Logging.Mediator.Tests
{
    [TestFixture]
    public class LoggingHandlerTests
    {
        [Test]
        public void HandleAsync_XX_XX()
        {
            var handlerMock = new Mock<IRequestHandler<TestRequest>>();
            handlerMock.Setup(x => x.HandleAsync(It.IsAny<TestRequest>()));

            var request = new TestRequest();

            var response = handlerMock.Object.HandleAsync(request);


        }
    }

    class TestRequest : IRequest
    {

    }
}
