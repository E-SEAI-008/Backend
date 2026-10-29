using BudgetTrackerApi.Models;

namespace BudgetTrackerApi.Dtos;

public record TransactionResponseDto(
    Guid Id,
    DateTimeOffset Timestamp,
    TransactionType Type,
    string Description,
    decimal Amount
);