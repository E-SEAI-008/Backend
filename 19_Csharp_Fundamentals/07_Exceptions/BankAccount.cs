public class BankAccount
{
    public string Owner { get; } = string.Empty;
    public decimal Balance { get; private set; } = 100;
    public BankAccount(string owner)
    {
        Owner = owner;
    }
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficentFundsException(
                $"Attempted to withdraw {amount}, but balance is {Balance}",
                Balance, // extra content
                amount // extra content
            );
        }

        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException($"Are you crazy?... Nice try with {amount}");
        }

        Balance += amount;
    }
}