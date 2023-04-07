using System.Net;
using System.Text.Json;
using HallOfFame.Core.Exceptions;
using HallOfFame.Core.ExternalServices;

namespace HallOfFame.Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context,
        IAppLogger<CustomExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception,
        IAppLogger<CustomExceptionHandlerMiddleware> logger)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case HallOfFameValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case HallOfFameNotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = notFoundException.Message;
                break;
            default:
                logger.LogError($"Unhandled exception: {exception.Message}");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (code == HttpStatusCode.InternalServerError)
        {
            return;
        }

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new {errpr = exception.Message});
        }

        await context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        return builder;
    }
}