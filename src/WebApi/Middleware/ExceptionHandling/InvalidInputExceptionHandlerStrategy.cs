using System.Net;
using System.Text.Json;

namespace WebApi.Middleware.ExceptionHandling
{
    public class InvalidInputExceptionHandlerStrategy : IExceptionHandlerStrategy
    {
        public Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;

            var response = new
            {
                message = "Invalid Input",
            };

            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }
    }
}
