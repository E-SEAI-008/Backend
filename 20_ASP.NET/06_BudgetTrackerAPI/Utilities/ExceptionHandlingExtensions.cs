namespace BudgetTrackerApi.Utilities;

public static class ExceptionHandlingExtensions
{
    public static WebApplication UseApiExceptionHandling(
        this WebApplication app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            StatusCodeSelector = exception =>
                exception is BadHttpRequestException badRequest
                    ? badRequest.StatusCode
                    : StatusCodes.Status500InternalServerError
        });

        return app;
    }
}