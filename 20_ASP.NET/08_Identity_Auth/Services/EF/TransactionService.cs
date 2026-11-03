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

    public async Task<IReadOnlyList<Transaction>> ListAsync(Guid userId)
        => await _db.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();

    public async Task<Transaction?> GetAsync(Guid userId, Guid id)
        => await _db.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    public async Task<Transaction> CreateAsync(Guid userId, CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
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

    public async Task<Transaction?> UpdateAsync(Guid userId, Guid id, UpdateTransactionDto dto)
    {
        var transaction = await _db.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

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

    public async Task<bool> DeleteAsync(Guid userId, Guid id)
    {
        var transaction = await _db.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (transaction is null) return false;

        _db.Transactions.Remove(transaction);
        await _db.SaveChangesAsync();
        return true;
    }
}
