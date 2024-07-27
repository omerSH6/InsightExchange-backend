using System.Net;
using System.Text.Json;

namespace WebApi.Middleware.ExceptionHandling
{
    public class OperationFailedExceptionHandlerStrategy : IExceptionHandlerStrategy
    {
        public Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "Operation Failed",
            };

            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }
    }
}
