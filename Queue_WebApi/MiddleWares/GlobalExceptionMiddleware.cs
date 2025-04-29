using Queue_WebApi.Exceptions;
using Serilog;
using System.Net;

namespace Queue_WebApi.MiddleWares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next) { _next = next; }    
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = exception switch
            {
                AppointmentNotFoundException _ => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;
            var result = new
            {
                errorMessage = exception.Message,
            };
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
