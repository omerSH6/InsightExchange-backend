namespace WebApi.Middleware.ExceptionHandling
{
    public interface IExceptionHandlerStrategy
    {
        Task HandleAsync(HttpContext context, Exception exception);
    }
}
