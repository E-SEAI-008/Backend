// var toyota = new Car("Toyota");
// var honda = new Car("Honda");

// toyota.Brand = "not toyota";

// toyota.Drive();
// honda.Drive();

// var person1 = new Person("John");
// var person2 = new Person("Jane");
// // person1.Name = "Bob";

// Console.WriteLine(person1.Name);
// Console.WriteLine(person2.Name);

// var box = new SecretBox();
// box.Code = "123";
// Console.WriteLine(box.Code);

var account = new BankAccount("John", 500);
var account2 = new BankAccount("Jane", 600);

// account.Balance = -9999999;

Console.WriteLine(account.Balance);
account.Deposit(200);
Console.WriteLine(account.Balance);


Console.WriteLine(account2.Balance);