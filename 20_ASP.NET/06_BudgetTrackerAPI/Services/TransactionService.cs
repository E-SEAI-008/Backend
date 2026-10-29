using BudgetTrackerApi.Dtos;
using BudgetTrackerApi.Infrastructure;
using BudgetTrackerApi.Interfaces;
using BudgetTrackerApi.Models;
using BudgetTrackerApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BudgetTrackerApi.Services;

public sealed class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _db;
    public TransactionService(ApplicationDbContext db) => _db = db;

    // GET Transaction by ID
    public async Task<Transaction?> GetAsync(Guid id)
        => await _db.Transactions.FindAsync(id);

    // List All Transactions + optional filtering
    public async Task<IReadOnlyList<Transaction>> ListAsync(
        DateOnly? from, DateOnly? to, TransactionType? type)
    {
        var query = _db.Transactions.AsNoTracking().AsQueryable();

        if (from.HasValue)
        {
            var start = new DateTimeOffset(
                from.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));

            query = query.Where(transaction => transaction.Timestamp >= start);
        }

        if (to.HasValue)
        {
            var end = new DateTimeOffset(
                to.Value.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc));

            query = query.Where(transaction => transaction.Timestamp <= end);
        }

        if (type.HasValue)
        {
            query = query.Where(transaction => transaction.Type == type.Value);
        }

        return await query
            .OrderByDescending(transaction => transaction.Timestamp)
            .ThenBy(transaction => transaction.Id)
            .ToListAsync();
    }
    // POST Transaction
    public async Task<Transaction> CreateAsync(
        TransactionType type, string description, decimal amount, DateOnly date)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Timestamp = new DateTimeOffset(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)),
            Type = type,
            Description = description.Trim(),
            Amount = amount
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();

        return transaction;
    }
    // PUT: Update an existing Transaction
    public async Task<Transaction?> UpdateAsync(
        Guid id, TransactionType type, string description, decimal amount, DateOnly date)
    {
        var transaction = await _db.Transactions.FindAsync(id);
        if (transaction is null) return null; // throw Exception

        transaction.Timestamp = new DateTimeOffset(
            date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        transaction.Type = type;
        transaction.Description = description.Trim();
        transaction.Amount = amount;

        await _db.SaveChangesAsync();
        return transaction;
    }
    // DELETE: an existing Transaction
    public async Task<bool> DeleteAsync(Guid id)
    {
        var transaction = await _db.Transactions.FindAsync(id);
        if (transaction is null) return false;

        _db.Transactions.Remove(transaction);
        await _db.SaveChangesAsync();
        return true;
    }

    // FOR REPORT
    public async Task<ReportResponseDto> GetReportAsync(
        DateOnly from, DateOnly to, TransactionType? type)
    {
        var transactions = await ListAsync(from, to, type);

        var totalIncome = transactions
            .Where(transaction => transaction.Type == TransactionType.Income)
            .Sum(transaction => transaction.Amount);

        var totalExpenses = transactions
            .Where(transaction => transaction.Type == TransactionType.Expense)
            .Sum(transaction => transaction.Amount);

        var items = transactions
            .Select(transaction => transaction.ToResponseDto())
            .ToList();

        return new ReportResponseDto(
            from, to, type,
            totalIncome, totalExpenses, totalIncome - totalExpenses,
            items);
    }
}