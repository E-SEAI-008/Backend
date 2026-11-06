using BudgetApi.Models;
using BudgetApi.Dtos.Transactions;

namespace BudgetApi.Services.Interfaces;

public interface ITransactionService
{
    Task<IReadOnlyList<Transaction>> ListAsync(Guid userId);
    Task<Transaction?> GetAsync(Guid userId, Guid id);
    Task<Transaction> CreateAsync(Guid userId, CreateTransactionDto dto);
    Task<Transaction?> UpdateAsync(Guid userId, Guid id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(Guid userId, Guid id);
}