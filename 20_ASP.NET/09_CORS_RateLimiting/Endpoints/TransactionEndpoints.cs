
using System.Security.Claims;
using BudgetApi.Dtos.Transactions;
using BudgetApi.Extensions;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;

namespace BudgetApi.Api.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .RequireRateLimiting("per-user");

        // GET all transactions
        group.MapGet("/", async (ClaimsPrincipal user, ITransactionService service) =>
        {
            var transactions = await service.ListAsync(user.GetUserId());
            return Results.Ok(transactions.Select(ToResponse));
        })
        .WithSummary("List All Transactions");

        // GET transaction by ID
        group.MapGet("/{id:guid}", async (
            Guid id,
            ClaimsPrincipal user,
            ITransactionService service) =>
        {
            var transaction = await service.GetAsync(user.GetUserId(), id);
            if (transaction is null) return Results.NotFound();
            return Results.Ok(ToResponse(transaction));
        });

        // POST a new transaction
        group.MapPost("/", async (
            CreateTransactionDto dto,
            ClaimsPrincipal user,
            ITransactionService service) =>
        {
            var transaction = await service.CreateAsync(user.GetUserId(), dto);
            return Results.Created($"/transactions/{transaction.Id}", ToResponse(transaction));
        })
        .WithSummary("Create a Transaction")
        .WithDescription("Add income or an expense with user-selected date")
        .Produces<TransactionResponseDto>(StatusCodes.Status201Created);

        // PATCH partially update an existing transaction
        group.MapPatch("/{id:guid}", async (
            Guid id, ClaimsPrincipal user,
            UpdateTransactionDto dto,
            ITransactionService service) =>
        {
            var transaction = await service.UpdateAsync(user.GetUserId(), id, dto);
            if (transaction is null) return Results.NotFound();
            return Results.Ok(ToResponse(transaction));
        });

        // DELETE an existing transaction
        group.MapDelete("/{id:guid}", async (
            Guid id,
            ClaimsPrincipal user,
            ITransactionService service) =>
        {
            var deleted = await service.DeleteAsync(user.GetUserId(), id);
            if (!deleted) return Results.NotFound();
            return Results.NoContent();
        });
    }
    // Response DTO shorter:
    private static TransactionResponseDto ToResponse(Transaction transaction)
        => new(
            transaction.Id,
            transaction.Type,
            transaction.Description,
            transaction.Amount,
            transaction.Timestamp,
            transaction.Date
            );
}