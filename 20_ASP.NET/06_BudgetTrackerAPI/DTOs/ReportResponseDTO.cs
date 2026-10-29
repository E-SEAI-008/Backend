using BudgetTrackerApi.Models;

namespace BudgetTrackerApi.Dtos;

public record ReportResponseDto(
    DateOnly From,
    DateOnly To,
    TransactionType? Type,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance,
    IReadOnlyList<TransactionResponseDto> Transactions
);