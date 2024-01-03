using System.Net;
using Task2_api.Services;
using System.Text.Json;

namespace Task2_api.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _loggerService; //dependency on logger service injected here

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            _loggerService.Write(message); //using logger service to write to console, instead of hardcoding logging method

            await _next(context);

            message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode;
            _loggerService.Write(message); //logger service used for response

        }
        catch (Exception ex)
        {
            await HandleException(context, ex); //calls exception handler if error occurs
            
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string message = "[Error] Http " + context.Request.Method + " - " + context.Response.StatusCode + " Error message" + ex.Message;
        _loggerService.Write(message); //logger service used for error logging

        var result = JsonSerializer.Serialize(ex.Message); //serializing message to json

        return context.Response.WriteAsync(result); //async returnin json formatted message
    }


}
   public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }