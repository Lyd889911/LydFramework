using static System.Net.Mime.MediaTypeNames;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 响应流中间件，所有中间件最前面
/// </summary>
public class ResponseStreamMiddleware
{
    private readonly RequestDelegate _next;
    public ResponseStreamMiddleware(RequestDelegate next)
    {
        this._next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var originalResponseStream = context.Response.Body;
        using var ms = new MemoryStream();
        context.Response.Body = ms;

        await _next.Invoke(context);

        ms.Position = 0;

        await ms.CopyToAsync(originalResponseStream);
        context.Response.Body = originalResponseStream;

    }
}