namespace WebApi.Middleware.ExceptionHandling
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ExceptionHandlerContext? _handlerContext;


        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _handlerContext = new ExceptionHandlerContext();
                var strategy = _handlerContext.GetStrategy(ex);
                await strategy.HandleAsync(httpContext, ex);
            }
        }
    }
}
