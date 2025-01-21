using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Shared.Middleware;

public class ExceptionLoggingMiddleware
{
    /// <summary>
    /// Problem details content type. See https://www.rfc-editor.org/rfc/rfc7807#section-6.1
    /// </summary>
    private const string ProblemDetailsContentType = "application/problem+json";
    
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
        
    /// <summary>
    /// Invokes the HTTP context
    /// </summary>
    public async Task Invoke(HttpContext context, ILogger<ExceptionLoggingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error at {Url}", context.Request.GetDisplayUrl());
                
            await SetResponse(context);
        }
    }
    
    /// <summary>
    /// Returns a serialized <see cref="Microsoft.AspNetCore.Mvc.ProblemDetails" /> when an unexpected error occurs.
    /// </summary>
    private async Task SetResponse(HttpContext context)
    {
        var problemDetails = JsonSerializer.Serialize(
            new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError,
            });

        context.Response.ContentType = ProblemDetailsContentType;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync(problemDetails);
    }
}