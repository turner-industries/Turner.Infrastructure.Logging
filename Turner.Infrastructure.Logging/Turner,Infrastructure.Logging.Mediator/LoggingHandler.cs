using System.Collections.Generic;
using System.Threading.Tasks;
using Turner.Infrastructure.Mediator;

namespace Turner.Infrastructure.Logging.Mediator
{
    public class LoggingHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        private readonly ILogger _logger;
        private readonly IRequestHandler<TRequest> _inner;

        public LoggingHandler(ILogger logger, IRequestHandler<TRequest> inner)
        {
            _logger = logger;
            _inner = inner;
        }

        public Task<Response> HandleAsync(TRequest request)
        {
            var response = _inner.HandleAsync(request);

            _logger.Info($"Executed {TypeUtility.GetPrettyName(typeof(TRequest))}",
                new LoggingResult<TRequest>
                {
                    Errors = response.Result.Errors,
                    Result = response.Result,
                    Request = request,
                });

            return response;
        }
    }

    public class LoggingHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        private readonly IRequestHandler<TRequest, TResult> _inner;
        private readonly ILogger _logger;

        public LoggingHandler(IRequestHandler<TRequest, TResult> inner, ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public Task<Response<TResult>> HandleAsync(TRequest request)
        {
            var response = _inner.HandleAsync(request);

            _logger.Info($"Executed {TypeUtility.GetPrettyName(typeof(TRequest))}",
                new LoggingResult<TRequest, TResult>
                {
                    Errors = response.Result.Errors,
                    Result = response.Result.Data,
                    Request = request,
                });

            return response;
        }
    }

    public class LoggingResult<TRequest>
    {
        public TRequest Request { get; set; }
        public List<Error> Errors { get; set; }
        public Response Result { get; set; }
    }

    public class LoggingResult<TRequest, TResult>
    {
        public TRequest Request { get; set; }
        public List<Error> Errors { get; set; }
        public TResult Result { get; set; }
    }
}
