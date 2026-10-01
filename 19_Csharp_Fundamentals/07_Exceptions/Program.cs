// var calculator = new Calculator();

// calculator.Divide(10, 0);

// Console.WriteLine("will this line run?");

// var calc = new Calculator();

// try
// {
//     var result = calc.Divide(10, 0);
//     Console.WriteLine($"Result: {result}");
// }
// catch (DivideByZeroException ex)
// {
//     Console.WriteLine($"Error: {ex.Message}");
// }

// Console.WriteLine("will this line run?");

// IndexOutOfRangeException
// var numbers = new int[] { 1, 2, 3 };
// Console.WriteLine(numbers[5]);

// void SetAge(int age)
// {
//     if (age < 0)
//     {
//         throw new ArgumentException();
//     }
// }

// SetAge(-10);

// void DoSomething()
// {
//     throw new Exception("Something went wrong inside DoSomething()");
// }

// try
// {
//     DoSomething();
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex.Message);
// }

// var account = new BankAccount();
// account.Withdraw(101);
// Console.WriteLine("Withdrawal successfull");
// Console.WriteLine($"Current Balance: {account.Balance}");

// var account = new BankAccount();

// try
// {
//     account.Withdraw(200);
// }
// catch (InsufficentFundsException ex)
// {
//     Console.WriteLine($"Error: {ex.Message}");
//     Console.WriteLine($"Balance = {ex.CurrentBalance}, Attempted = {ex.AttemptedWithdrawal}");
// }

var account = new BankAccount("Costa");

// DEPOSIT
try
{
    account.Deposit(100);
    Console.WriteLine($"Deposit successfull, new balance: {account.Balance}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Account owner: {account.Owner}");
    Console.WriteLine($"Error: {ex.Message}");
}

// WITHDRAW
try
{
    account.Withdraw(250);
    Console.WriteLine($"Withdrawal successfull, new balance: {account.Balance}");
}
catch (InsufficentFundsException ex)
{
    Console.WriteLine($"Account owner: {account.Owner}");
    Console.WriteLine($"Error: {ex.Message}");
    Console.Write($"Balance: {ex.CurrentBalance}, Attempted: {ex.AttemptedWithdrawal}");
}