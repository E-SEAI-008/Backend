Before we write any C#, we need to set up our machine

## Install the .NET SDK

We install the latest **LTS** (Long-Term Support) version:

1. Go to https://dotnet.microsoft.com/en-us/download
2. Pick the latest **LTS** version (at the time of writing, **.NET 10**)
3. Run the installer and follow the wizard
4. Check it worked — open a terminal:

```bash
dotnet --version    # → n.x.x  (n = the LTS major version)
```

If you see a version number, you're good to go.

## Configure VS Code

Install the **C# Dev Kit** extension:

- Open the Extensions panel in VS Code
- Search for **C# Dev Kit** (by Microsoft) and install it

This gives you IntelliSense, refactoring, an integrated debugger, and unit-test discovery for C# and .NET.

> **Note:** _Visual Studio_ (the full IDE) is Windows-only. We're using **VS Code**, which works everywhere.

## First Console App

Create a new project

```bash
dotnet new console -o HelloThere
```

What does this mean?

- dotnet → the .NET CLI tool
- new → create something new (like npm init)
- console → the project template (console app)
- -o HelloThere → output folder name

very similar to `npm init`
but instead of asking questions, .NET uses templates.

run once => `dotnet run`
watch mode => `dotnet watch run`

You can also select Debug mode.

### Program.cs - Application Entry Point

- Program.cs is where our application starts running
- when we run `dotnet run` .NET looks for Program.cs
- and executes the code inside it

### Default Program.cs (Console App)

```csharp
Console.WriteLine("Hello, World!");

// Console => built-in .NET class
// WriteLine => method that prints text to the terminal
// "Hello, World!" => string value
// This line runs from top to bottom, just like JavaScript.
```

## Data Types

Some of the most common built-in types:

| Type     | Description           | Example                         | In words                               |
| -------- | --------------------- | ------------------------------- | -------------------------------------- |
| `int`    | 32-bit whole numbers  | `int age = 30;`                 | numbers with no decimals: 1, 2, 42, -7 |
| `long`   | 64-bit whole numbers  | `long population = 8000000000;` | same, but for HUGE numbers             |
| `float`  | 32-bit decimal number | `float temperature = 36.6F;`    | numbers with decimals, less precise    |
| `double` | 64-bit decimal number | `double price = 9.99;`          | numbers with decimals, more precise    |
| `bool`   | true / false          | `bool isActive = true;`         | yes or no, on or off                   |
| `string` | text                  | `string name = "Alice";`        | any text, in double quotes ""          |
| `char`   | single character      | `char grade = 'A';`             | exactly one character, in single ''    |

### What do these terms mean??

**Whole numbers vs decimal numbers**

- **Whole numbers** → no fractional part: `1`, `2`, `100`, `-5`
- **Decimal numbers** → have a fractional part: `9.99`, `36.6`, `3.14`

**What's "32-bit" / "64-bit"?**

It's how much memory the number takes up, which decides how big (or how precise) it can be.

- **32-bit** → smaller box, holds smaller numbers
- **64-bit** → bigger box, holds much bigger (or more precise) numbers

Think of it like buckets:

- `int` (32-bit) → holds up to ~2 billion. Fine for age, count, quantity.
- `long` (64-bit) → holds up to ~9 quintillion. For when `int` isn't enough.
- `float` (32-bit) → ~7 digits of precision. "Good enough" decimals.
- `double` (64-bit) → ~15–16 digits of precision. The default for decimals.

### Numeric Suffixes

- `F` → marks a `float`
- `D` → marks a `double` (optional)
- Without suffix, decimals default to `double`

> **Rule of thumb:** use `int` for whole numbers and `double` for decimals. Reach for `long` or `float` only when you have a specific reason.

```csharp
int age = 30;
var year = 2026;        // inferred as int

long population = 8000000000;
var distance = 1.5e8;  // inferred as double

double price = 9.99;
var temperature = 36.6;   // inferred as double

float ratio = 0.75F;
var speed = 120.5F;       // inferred as float

bool isActive = true;
var isAdmin = false;   // inferred as bool

string city = "Rome";
var country = "Italy";   // inferred as string

char grade = 'A';
var letter = 'Z';   // inferred as char


// -----------------
var x = 10;        // int
var y = 9.99;      // double
var z = "Hello";   // string
var ok = true;     // bool

// The value on the right decides the type on the left.
```

## OPERATORS

Operators are symbols that perform operations on values and variables.

C# operators are **type-safe**, meaning:

- operations must make sense for the given types
- invalid operations fail at **compile time**

### Arithmetic Operators

Used for mathematical operations.

| Operator | Meaning            |
| -------- | ------------------ |
| `+`      | addition           |
| `-`      | subtraction        |
| `*`      | multiplication     |
| `/`      | division           |
| `%`      | remainder (modulo) |

---

### Comparison Operators

Used to compare values.  
The result is always a `bool`.

| Operator | Meaning               |
| -------- | --------------------- |
| `==`     | equal                 |
| `!=`     | not equal             |
| `<`      | less than             |
| `>`      | greater than          |
| `<=`     | less than or equal    |
| `>=`     | greater than or equal |

---

### Logical Operators

Used to combine boolean expressions.

| Operator | Meaning |
| -------- | ------- |
| `&&`     | AND     |
| `\|\|`   | OR      |
| `!`      | NOT     |

---

### Assignment Operators

Used to assign and update values.
Assignment operators are just shortcuts for updating a variable using its current value.

| Operator | Example  | Meaning     |
| -------- | -------- | ----------- |
| `=`      | `x = 5`  | assign      |
| `+=`     | `x += 2` | `x = x + 2` |
| `-=`     | `x -= 1` | `x = x - 1` |
| `*=`     | `x *= 3` | `x = x * 3` |
| `/=`     | `x /= 2` | `x = x / 2` |

---
