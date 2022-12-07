namespace FinanceApp.WebApi.Middleware.CustomExpectionsHandler;

public static class CustomExceptionsHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionsHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionsHandlerMiddleware>();
    }
}