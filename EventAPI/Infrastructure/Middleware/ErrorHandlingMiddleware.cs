using System.Text.Json;
using System.Net;

namespace EventAPI.Infrastructure.Middleware
{

    /// <summary>
    /// Middleware that intercept all exceptions not being catched and return a json response with the exception details
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate del;


        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate del)
        {
            this.logger = logger;
            this.del = del;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await del(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string content
                     = JsonSerializer.Serialize(new { code = (int)HttpStatusCode.InternalServerError, message = ex.Message, stacktrace = ex.StackTrace });

                await context.Response.WriteAsync(content);
            }
        }
    }
}
