Quick reference

| Concept        | Syntax                   | Meaning                    |
| -------------- | ------------------------ | -------------------------- |
| Generic method | `T Identity<T>(T value)` | one method, any type       |
| Generic class  | `class Box<T>`           | one class, any type        |
| Constraint     | `where T : Entity`       | T must inherit `Entity`    |
| Combined       | `where T : class, new()` | reference type + creatable |
