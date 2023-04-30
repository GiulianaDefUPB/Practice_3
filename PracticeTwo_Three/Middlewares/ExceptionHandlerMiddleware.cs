using System.Globalization;

namespace UPB.PracticeTwo_Three.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
        catch (System.Exception ex)
        {
            // Log ex.Message
            HandleException(context, ex);
        }

    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "text/json";
        // context.Response.StatusCode = 500;
        return context.Response.WriteAsync("Error ocurred: " + ex.Message);//Escribir la respuesta
    }
}

public static class ExceptionHandlerExtensions //sufijo extensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}