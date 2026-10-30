using System.Transactions;
using BudgetApi.Dtos.Reports;
using BudgetApi.Infrastructure;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApi.Services.EF;

public sealed class ReportService : IReportService
{
    private readonly ApplicationDbContext _db;
    public ReportService(ApplicationDbContext db) => _db = db;

    public async Task<SummaryReportResponseDto> GetSummaryAsync(
        DateOnly start, DateOnly end)

    {
        var transactions = await _db.Transactions
            .Where(transaction => transaction.Date >= start && transaction.Date <= end)
            .ToListAsync();
        // 2026-10-01 && 2026-10.30

        var income = transactions.Where(transaction => transaction.Type == TransactionType.Income)
            .Sum(transaction => transaction.Amount);

        var expense = transactions.Where(transaction => transaction.Type == TransactionType.Expense)
            .Sum(transaction => transaction.Amount);

        return new SummaryReportResponseDto(start, end, income, expense, income - expense);
    }
}