using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;

namespace WebApi.Middleware
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

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
            catch (InvalidInputException)
            {
                await HandleInvalidInputExceptionAsync(httpContext);
            }
            catch (OperationFailedException)
            {
                await HandleOperationFailedExceptionAsync(httpContext);
            }            
            catch (UnauthorizedException)
            {
                await HandleUnauthorizedExceptionAsync(httpContext);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, false);
            }
        }

        private static async Task HandleInvalidInputExceptionAsync(HttpContext context)
        {
            await HandleGeneralExceptionAsync(context, new Exception("Invalid Input"), HttpStatusCode.NotAcceptable, true);
        } 
        
        private static async Task HandleOperationFailedExceptionAsync(HttpContext context)
        {
            await HandleGeneralExceptionAsync(context, new Exception("Operation Failed"), HttpStatusCode.InternalServerError, true);
        }
        
        private static async Task HandleUnauthorizedExceptionAsync(HttpContext context)
        {
            await HandleGeneralExceptionAsync(context, new Exception("Unauthorized"), HttpStatusCode.Unauthorized, true);
        }


        private static Task HandleGeneralExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode, bool showExceptionMessage)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            var response = new
            {
                message = "An unexpected error occurred.",
                details = showExceptionMessage? exception.Message : string.Empty,
            };

            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }

    }
}
