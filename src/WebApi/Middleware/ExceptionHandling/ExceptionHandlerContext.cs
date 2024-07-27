using Application.Common.Exceptions;

namespace WebApi.Middleware.ExceptionHandling
{
    public class ExceptionHandlerContext
    {
        private readonly IDictionary<Type, IExceptionHandlerStrategy> _strategies;
        public IExceptionHandlerStrategy GeneralStrategy { get; }

        public ExceptionHandlerContext()
        {
            _strategies = new Dictionary<Type, IExceptionHandlerStrategy>
        {
            { typeof(InvalidInputException), new InvalidInputExceptionHandlerStrategy() },
            { typeof(OperationFailedException), new OperationFailedExceptionHandlerStrategy() },
            { typeof(UnauthorizedException), new UnauthorizedExceptionHandlerStrategy() }
        };

            GeneralStrategy = new GeneralExceptionHandlerStrategy();
        }


        public IExceptionHandlerStrategy GetStrategy(Exception exception)
        {
            return _strategies.TryGetValue(exception.GetType(), out var strategy) ? strategy : GeneralStrategy;
        }
    }
}
