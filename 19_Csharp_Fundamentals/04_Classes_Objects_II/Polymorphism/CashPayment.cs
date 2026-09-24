public class CashPayment : PaymentMethod
{
    public override void Pay(decimal amount)
    {
        Console.WriteLine($"Paying {amount:c} in cash");
    }
}