public class BankAccount
{
    private decimal _balance;
    public BankAccount(string owner, decimal balance)
    {
        Owner = owner;
        Balance = balance;
    }

    public string Owner { get; set; }
    public decimal Balance
    {
        get
        {
            return _balance;
        }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Balance cannot be negative!");
            }

            _balance = value;
        }
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
}