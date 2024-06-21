using Microsoft.Extensions.Primitives;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("Content-Security-Policy", new StringValues("default-src 'self'"));
        context.Response.Headers.Append("X-Content-Type-Options", new StringValues("nosniff"));
        context.Response.Headers.Append("X-Frame-Options", new StringValues("SAMEORIGIN"));
        context.Response.Headers.Append("X-XSS-Protection", new StringValues("1; mode=block"));
        context.Response.Headers.Append("Strict-Transport-Security", new StringValues("max-age=31536000; includeSubDomains"));
        context.Response.Headers.Append("Referrer-Policy", new StringValues("no-referrer"));
        context.Response.Headers.Append("Feature-Policy", new StringValues("geolocation 'none';midi 'none';notifications 'none';push 'none';sync-xhr 'none';microphone 'none';camera 'none';magnetometer 'none';gyroscope 'none';speaker 'none';vibrate 'none';fullscreen 'self';payment 'none'"));
        // context.Response.Headers.Append("Content-Type", new StringValues("text/html; charset=utf-8"));
        await _next(context);
    }
}