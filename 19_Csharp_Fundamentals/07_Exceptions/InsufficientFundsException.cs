public class InsufficentFundsException : Exception
{
    public decimal CurrentBalance { get; }
    public decimal AttemptedWithdrawal { get; }

    public InsufficentFundsException(string message, decimal balance, decimal attempted)
    : base(message)
    {
        CurrentBalance = balance;
        AttemptedWithdrawal = attempted;
    }
}