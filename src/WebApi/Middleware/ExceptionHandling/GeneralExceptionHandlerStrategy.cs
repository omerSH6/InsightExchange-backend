using System.Net;
using System.Text.Json;

namespace WebApi.Middleware.ExceptionHandling
{
    public class GeneralExceptionHandlerStrategy : IExceptionHandlerStrategy
    {
        public Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "An unexpected error occurred.",
            };

            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }
    }
}
