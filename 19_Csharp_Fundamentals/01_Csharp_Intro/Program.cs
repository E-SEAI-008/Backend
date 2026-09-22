// VARIABLES

// int counter;
// counter = 10;

// or
// int number = 10;
// var number2 = 15;

// Console.WriteLine(number);
// Console.WriteLine(number2);
// Console.WriteLine(number.GetType().Name);

// string name = "Laissa";
// var name2 = "Zekeriye";

// Scientific Notation
// double a = 1.5e1; // 1.5 x 10 = 15
// double b = 1.5e3; // 1.5 x 10³ = 1500
// double c = 1.5e8; // 1.5 x 10(8) = 150 000 000

// Console.WriteLine(a);
// Console.WriteLine(b);
// Console.WriteLine(c);

// // MORE
// int age = 30;
// var year = 2026; // inferred as int

// long population = 8000000000;
// var distance = 1.5e2;

// double price = 9.99;
// var temprature = 36.6; // inferred as double

// bool isActive = true;
// var isAdmin = false; // inferred as bool

// string city = "Rome";
// var country = "Italy"; // inferred as string

// char grade = "A";
// var letter = "z"; // inferred as char

// OPERATORS
// Arithmetic Operators

// int a = 10;
// int b = 3;

// Console.WriteLine(a + b); // 13
// Console.WriteLine(a - b); // 7
// Console.WriteLine(a * b); // 30
// Console.WriteLine(a / b); // 3
// Console.WriteLine(a % b); // 1

// string + operator
// string message = "Hello";
// int age = 30;

// Console.WriteLine(message + " I am " + age + ".");
// Console.WriteLine($"{message} I am {age}.");

// Comparison Operators
// int x = 5;
// int y = 10;

// Console.WriteLine(x == y); // false
// Console.WriteLine(x < y); // true
// Console.WriteLine(x > y); // false
// Console.WriteLine(x != y); // true
// Console.WriteLine(x >= y); // false

// Logical Operators
// bool isAdult = true;
// bool hasTicket = false;

// Console.WriteLine(isAdult && hasTicket); // false
// Console.WriteLine(isAdult || hasTicket); // true
// Console.WriteLine(!isAdult); // false

// Assignment Operators
// int counter = 10; // set

// counter += 5; // 15 => add to
// counter -= 3; // 7 => subtract from
// counter *= 2; // 20 => multiply
// counter /= 5; // 2 => divideby

// Console.WriteLine(counter);

// CONDITIONALS
// IF / ELSE

// int age = 17;

// if (age >= 18)
// {
//     Console.WriteLine("You are an adult");
// }
// else
// {
//     Console.WriteLine("You are underage");
// }

// ELSE IF
// if (age < 13)
// {
//     Console.WriteLine("Child");
// }
// else if (age < 18)
// {
//     Console.WriteLine("Teenager");
// }
// else
// {
//     Console.WriteLine("Adult");
// }

// int number = 8;

// // check if the number is even or odd using the modulus operator (%)
// // % gives the remainder of a division

// // If the remaining of number divided by 2 is 0, the number is even
// // otherwise, the condition is false, "Odd" will be assigned

// string result = number % 2 == 0 ? "Even" : "Odd";
// Console.WriteLine($"{number} is {result}");

// About %
// Console.WriteLine(7 % 2);
// Console.WriteLine(6 % 2);
// Console.WriteLine(4 % 2);
// Console.WriteLine(12 % 2);

// SWITCH
// string grade = "B";

// switch (grade)
// {
//     case "A":
//         Console.WriteLine("Excellent!");
//         break;
//     case "B":
//         Console.WriteLine("Good");
//         break;
//     case "C":
//         Console.WriteLine("Avarage");
//         break;
//     default:
//         Console.WriteLine("Needs improvements");
//         break;
// }

// string message = grade switch
// {
//     "A" => "Excellent!",
//     "B" => "Good",
//     "C" => "Avarage",
//     _ => "Needs improvement"
// };

// Console.WriteLine(message);

// LOOPS
// While Loop

// int count = 0;

// while (count < 5)
// {
//     Console.WriteLine($"Count is {count}");
//     count++;
// }

// DO while...
// int number = 15;

// do
// {
//     Console.WriteLine(number);
//     number++;
// } while (number < 5);



// do
// {
//     Console.WriteLine("Show login form");
// }while(!loginSuccessfull);


// some other stuff


// for loop
// for (int i = 0; i < 5; i++)
// {
//     Console.WriteLine($"Iteration {i}");
// }

// for (int i = 0; i < 10; i++)
// {
//     if (i == 3)
//     {
//         // skip this iteration and continue with the next value of i
//         continue;
//     }

//     if (i == 7)
//     {
//         break;
//     }

//     Console.WriteLine(i);
// }