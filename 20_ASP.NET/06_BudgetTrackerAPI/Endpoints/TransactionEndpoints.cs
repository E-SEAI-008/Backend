using BudgetTrackerApi.Dtos;
using BudgetTrackerApi.Interfaces;
using BudgetTrackerApi.Models;
using BudgetTrackerApi.Utilities;

namespace BudgetTrackerApi.Endpoints;

public static class TransactionEndpoints
{
    public static RouteGroupBuilder MapTransactions(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/transactions").WithTags("Transactions");

        // GET /transactions/{id} => GET ONE TRANSACTION
        group.MapGet("/{id:guid}", async (
            Guid id, ITransactionService service) =>
        {
            var transaction = await service.GetAsync(id);

            if (transaction is null)
                return Results.NotFound();

            return Results.Ok(transaction.ToResponseDto());
        });

        // GET /transactions => list with optional filters
        group.MapGet("/", async (
            ITransactionService service,
            DateOnly? from = null,
            DateOnly? to = null,
            TransactionType? type = null) =>
        {
            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                return Results.Problem(
                    statusCode: 400,
                    title: "Invalid date range",
                    detail: "The start date must be on or before the end date"
                );
            }

            if (type.HasValue && !Enum.IsDefined(type.Value))
            {
                return Results.Problem(
                    statusCode: 400,
                    title: "Invalid transaction type",
                    detail: "Use Income or Expense"
                );
            }

            var transactions = await service.ListAsync(from, to, type);

            var response = transactions
                .Select(transaction => transaction.ToResponseDto())
                .ToList();

            return Results.Ok(response);
        });

        // POST /transactions => CREATE A TRANSACTION
        group.MapPost("/", async (
            CreateTransactionDto dto, ITransactionService service) =>
        {
            var transaction = await service.CreateAsync(
                dto.Type!.Value,
                dto.Description,
                dto.Amount,
                dto.Date!.Value
            );

            return Results.Created(
                $"/transactions/{transaction.Id}",
                transaction.ToResponseDto());

        });

        // PUT /transactions/{id} => UPDATE A TRANSACTION
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateTransactionDto dto,
            ITransactionService service) =>
        {
            var transaction = await service.UpdateAsync(
                id,
                dto.Type!.Value,
                dto.Description,
                dto.Amount,
                dto.Date!.Value);

            if (transaction is null) return Results.NotFound();

            return Results.Ok(transaction.ToResponseDto());
        });

        // DELETE /transactions/{id} => DELETE A TRANSACTION
        group.MapDelete("/{id:guid}", async (
            Guid id, ITransactionService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (!deleted) return Results.NotFound();

            return Results.NoContent();
        });

        return group;
    }
}