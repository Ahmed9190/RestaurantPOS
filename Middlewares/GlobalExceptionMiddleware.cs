using System.Net;
using System.Text.Json;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Middlewares;

public class GlobalExceptionMiddleware
{
  private readonly RequestDelegate _next;

  public GlobalExceptionMiddleware(RequestDelegate next)
  {
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
      await HandleExceptionAsync(context, ex);
    }
  }

  private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    // Default to 500
    var statusCode = (int)HttpStatusCode.InternalServerError;
    var message = ex.Message;

    if (ex is ResourceNotFoundException)
      statusCode = (int)HttpStatusCode.NotFound;
    else if (ex is ValidationException)
      statusCode = (int)HttpStatusCode.BadRequest;

    var problemDetails = new
    {
      status = statusCode,
      error = ex.GetType().Name,
      message = message,
      timestamp = DateTime.UtcNow
    };

    var json = JsonSerializer.Serialize(problemDetails);
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(json);
  }
}
