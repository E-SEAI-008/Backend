using BudgetTrackerApi.Dtos;
using BudgetTrackerApi.Models;

namespace BudgetTrackerApi.Interfaces;

public interface ITransactionService
{
    Task<Transaction?> GetAsync(Guid Id);
    Task<IReadOnlyList<Transaction>> ListAsync(
        DateOnly? from, DateOnly? to, TransactionType? type);
    Task<Transaction> CreateAsync(
        TransactionType type,
        string description,
        decimal amount,
        DateOnly date
    );
    Task<Transaction?> UpdateAsync(
        Guid Id,
        TransactionType type,
        string description,
        decimal amount,
        DateOnly date
    );
    Task<bool> DeleteAsync(Guid id);
    Task<ReportResponseDto> GetReportAsync(
        DateOnly from,
        DateOnly to,
        TransactionType? type
    );
}