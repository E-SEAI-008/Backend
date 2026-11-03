using BudgetApi.Models;

namespace BudgetApi.Dtos.Transactions;

public record TransactionResponseDto(
    Guid Id,
    TransactionType Type,
    string Description,
    decimal Amount,
    DateTimeOffset Timestamp,
    DateOnly Date
);