using System.Net;
using System.Text.Json;

namespace WebApi.Middleware.ExceptionHandling
{
    public class UnauthorizedExceptionHandlerStrategy : IExceptionHandlerStrategy
    {
        public Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var response = new
            {
                message = "Unauthorized",
            };

            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }
    }
}
