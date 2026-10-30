using BudgetApi.Services.Interfaces;

namespace BudgetApi.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/reports").WithTags("Reports");

        group.MapGet("/", async (DateOnly start, DateOnly end, IReportService service) =>
        {
            if (start > end)
                return Results.Problem(
                    statusCode: 400,
                    title: "Invalid date range",
                    detail: "Start date must be on or before the end date"
                    );

            return Results.Ok(await service.GetSummaryAsync(start, end));
        });
    }
}