.NET already gives you many exception types

Before writing your own, check if one of these already fits:

https://learn.microsoft.com/en-us/dotnet/standard/exceptions/

| Exception                     | When it happens                                 |
| ----------------------------- | ----------------------------------------------- |
| `Exception`                   | the base class — all exceptions inherit from it |
| `IndexOutOfRangeException`    | you access an index that doesn't exist          |
| `NullReferenceException`      | you use an object that is `null`                |
| `InvalidOperationException`   | the operation isn't valid for the current state |
| `ArgumentException`           | an argument is invalid                          |
| `ArgumentNullException`       | an argument is `null` but shouldn't be          |
| `ArgumentOutOfRangeException` | an argument is outside the allowed range        |

Exception base class: https://learn.microsoft.com/en-us/dotnet/standard/exceptions/exception-class-and-properties
