using BudgetTrackerApi.Interfaces;
using BudgetTrackerApi.Models;


namespace BudgetTrackerApi.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReports(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/reports").WithTags("Reports");

        group.MapGet("/", async (
            DateOnly from,
            DateOnly to,
            ITransactionService service,
            TransactionType? type = null) =>
        {
            if (from > to)
            {
                return Results.Problem(statusCode: 400,
                    title: "Invalid date range",
                    detail: "The start date must be on or before the end date.");
            }

            if (type.HasValue && !Enum.IsDefined(type.Value))
            {
                return Results.Problem(statusCode: 400,
                    title: "Invalid transaction type",
                    detail: "Use Income or Expense.");
            }

            var report = await service.GetReportAsync(from, to, type);
            return Results.Ok(report);
        });

        return group;
    }
}