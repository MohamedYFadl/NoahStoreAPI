using Microsoft.Extensions.Caching.Memory;
using NoahStore.API.Errors;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace NoahStore.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _RateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment env,IMemoryCache memoryCache)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurity(context);
                 if (IsRequestAllowed(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response = new ApiResponse((int)HttpStatusCode.TooManyRequests, "Too Many requests, please try again later!");
                   await context.Response.WriteAsJsonAsync(response);
                }
                
                 await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                    new ExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    : new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }


        private  bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var casheKey = $"Key:{ip}";
            DateTime dateNow = DateTime.Now;

            var (timestamp, count) = _memoryCache.GetOrCreate(casheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _RateLimitWindow;
                return (timestamp: dateNow, count: 0);
            });

            if(dateNow - timestamp < _RateLimitWindow)
            {
                if(count >= 8 )
                {
                    return false;
                }
                _memoryCache.Set(casheKey, (timestamp, count += 1), _RateLimitWindow);
            }
            else
            {
                _memoryCache.Set(casheKey, (timestamp, count), _RateLimitWindow);
            }

            return true;

        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
