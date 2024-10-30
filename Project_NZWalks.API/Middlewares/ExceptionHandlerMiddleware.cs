using System.Net;

namespace Project_NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware(
        ILogger<ExceptionHandlerMiddleware> logger,
        RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //return custom error response
                httpContext.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong. We are" +
                    " looking in to resolving it."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
