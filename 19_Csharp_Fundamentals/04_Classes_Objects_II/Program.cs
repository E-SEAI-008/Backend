// var box1 = new PuzzleBox("John", "Silver Key");
// var box2 = new PuzzleBox("Jane", "Gold Key");

// box1.Open();
// box2.Secret = "Bronze Key";
// box2.Open();

// var account = new BankAccount("John", 1000);
// // account._balance = 9999999999999999;
// // account.Owner = "Jane";

// Console.WriteLine($"Balance: {account.GetBalance()}");
// account.Deposit(250);
// Console.WriteLine($"Balance: {account.GetBalance()}");

// var molly = new Dog("Molly");

// var garfield = new Cat("Garfield");

// molly.Eat();
// molly.Sleep();
// molly.Bark();

// garfield.Eat();
// garfield.Sleep();
// garfield.Meow();

// var basic = new Ticket("John", 20);
// var train = new TrainTicket("Jane", 15, "Berlin");
// var concert = new ConcertTicket("Costa", 50, "IRON Maiden");

// basic.PrintInfo();
// train.PrintTrainInfo();
// concert.PrintConcertInfo();

// var notifications = new List<Notification>
// {
//     new EmailNotification(),
//     new SmsNotification()
// };

// foreach (var notification in notifications)
// {
//     notification.Send();
// }


var payments = new List<PaymentMethod>
{
    new PaymentMethod(),
    new CardPayment(),
    new CashPayment(),
};

foreach (var payment in payments)
{
    payment.Pay(20);
}