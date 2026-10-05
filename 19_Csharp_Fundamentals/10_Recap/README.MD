### START

this app touches **almost everything we've learned in C#**

as we build it, we'll recap each topic:

| Building block | Recaps                                         |
| -------------- | ---------------------------------------------- |
| Model + status | classes/records, **enums**, value vs reference |
| Book list      | **collections** (`List<T>`)                    |
| Menu loop      | methods, **conditionals & loops**, `switch`    |
| Update status  | **records + `with`** (immutability)            |
| Save / load    | **File I/O** + **JSON serialisation**          |
| Reports        | **LINQ**                                       |
| Safety         | **exceptions**                                 |

> Think of it as the little sibling of the Budget Tracker

We'll split the app into **three responsibilities**:

```
📁 ReadingTracker/
├── 📄 Program.cs              ← just starts the app (the "bootstrapper")
├── 📁 Models/
│   ├── 📄 ReadingStatus.cs    ← the enum
│   └── 📄 Book.cs             ← the data (record)
└── 📁 Services/
    ├── 📄 BookStorage.cs      ← ALL file + JSON work lives here
    └── 📄 BookApp.cs          ← the menu loop and user interaction
```

### Final

Building this Reading Tracker, we touched:

| Topic (from the last 2 weeks)      | Where it showed up                          |
| ---------------------------------- | ------------------------------------------- |
| Console app / top-level statements | `Program.cs` bootstrap                      |
| **Enums**                          | `ReadingStatus`                             |
| **Records** / value vs reference   | `Book` record                               |
| **Records + `with`**               | `UpdateStatus` (immutable → changed copy)   |
| Classes / constructors             | `BookStorage`, `BookApp`                    |
| **Collections**                    | `List<Book>` (`Add`, `RemoveAt`)            |
| Methods, loops, `switch`           | `BookApp.Run()`                             |
| **File I/O**                       | `BookStorage` saving & loading              |
| **JSON serialisation**             | `Serialize` / `Deserialize`                 |
| **LINQ**                           | `ShowReport` (`Where`, `OrderBy`, `Select`) |
| **Exceptions**                     | safe loading in `BookStorage`               |
| Clean structure                    | Models / Services split                     |
