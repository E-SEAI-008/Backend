using BudgetTrackerApi.Dtos;
using BudgetTrackerApi.Models;

namespace BudgetTrackerApi.Utilities;

public static class TransactionMappings
{
    public static TransactionResponseDto ToResponseDto(
        this Transaction transaction)
    {
        return new TransactionResponseDto(
            transaction.Id,
            transaction.Timestamp,
            transaction.Type,
            transaction.Description,
            transaction.Amount);
    }
}