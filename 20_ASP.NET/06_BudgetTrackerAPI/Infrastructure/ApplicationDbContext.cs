
using BudgetTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();
}