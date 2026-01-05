using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

Console.WriteLine("=== PROGRAM STARTED ===");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 演示用 API Key（课程实验用，生产环境应使用更规范鉴权）
const string ApiKey = "123456";

// 仅保护 /hello
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Path.StartsWithSegments("/hello"))
    {
        if (!ctx.Request.Headers.TryGetValue("X-API-Key", out var key) || key != ApiKey)
        {
            ctx.Response.StatusCode = 401;
            await ctx.Response.WriteAsync("Unauthorized");
            return;
        }
    }
    await next();
});

app.MapGet("/hello", () => Results.Ok("hello"));

app.Run();
