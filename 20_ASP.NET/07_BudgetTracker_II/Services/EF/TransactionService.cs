using BudgetApi.Dtos.Transactions;
using BudgetApi.Infrastructure;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApi.Services.EF;

public sealed class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _db;
    public TransactionService(ApplicationDbContext db) => _db = db;

    public async Task<IReadOnlyList<Transaction>> ListAsync()
        => await _db.Transactions.ToListAsync();

    public async Task<Transaction?> GetAsync(Guid id)
        => await _db.Transactions.FindAsync(id);

    public async Task<Transaction> CreateAsync(CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Type = dto.Type!.Value,
            Description = dto.Description.Trim(),
            Amount = dto.Amount,
            Date = dto.Date!.Value,
            Timestamp = DateTimeOffset.UtcNow
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var transaction = await _db.Transactions.FindAsync(id);
        if (transaction is null) return null;

        if (dto.Description is not null)
            transaction.Description = dto.Description.Trim();
        if (dto.Amount.HasValue)
            transaction.Amount = dto.Amount.Value;
        if (dto.Date.HasValue)
            transaction.Date = dto.Date.Value;

        await _db.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var transaction = await _db.Transactions.FindAsync(id);
        if (transaction is null) return false;

        _db.Transactions.Remove(transaction);
        await _db.SaveChangesAsync();
        return true;
    }
}
