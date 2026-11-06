using Microsoft.AspNetCore.Identity;

namespace BudgetApi.Models;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;

    // one user can have many transactions
    public List<Transaction> Transactions { get; set; } = [];
}