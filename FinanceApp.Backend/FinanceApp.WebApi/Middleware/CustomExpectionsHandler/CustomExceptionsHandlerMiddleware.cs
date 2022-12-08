using System.Net;
using System.Text.Json;
using FinanceApp.Domain.Exceptions;
using FluentValidation;

namespace FinanceApp.WebApi.Middleware.CustomExpectionsHandler;

public class CustomExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionsHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExcepitonAsync(context, exception);
        }
    }

    private Task HandleExcepitonAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = "";

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case ForbiddenException:
                code = HttpStatusCode.Forbidden;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new {error = exception.Message});
        }

        return context.Response.WriteAsync(result);
    }
}