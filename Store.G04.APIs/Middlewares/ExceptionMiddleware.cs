using Store.G04.APIs.Errors;
using System.Text.Json;

namespace Store.G04.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var response = _env.IsDevelopment() ? new ApiServerErrorResponse(500,  ex.Message,ex.StackTrace.ToString())
                                                    : new ApiServerErrorResponse(500);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);


            }
        }
    }
}
