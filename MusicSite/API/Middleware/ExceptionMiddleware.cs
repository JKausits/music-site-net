using MusicSite.API.Exceptions;
using System.Net;
using System.Text.Json;

namespace MusicSite.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                object body = new { Messages = new[] { "Internal Server Error" } };

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        body = new { Messages = new[] { ex.Message } };
                        break;
                    case UnauthorizedException:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        body = new { Messages = new[] { ex.Message } };
                        break;
                    case BadRequestException badRequest:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        body = new { Messages = new[] { badRequest.Messages } };
                        break;
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(body, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
