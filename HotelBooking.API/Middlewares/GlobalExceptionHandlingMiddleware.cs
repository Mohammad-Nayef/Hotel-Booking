namespace HotelBooking.Api.Middlewares
{
    /// <summary>
    /// Handle any unhandled exception by returning 
    /// <see cref="StatusCodes.Status500InternalServerError"/> without message.
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception: {message}", ex);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
