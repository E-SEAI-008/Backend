| Type              | Returns a value? | Parameters            | Use for                              |
| ----------------- | ---------------- | --------------------- | ------------------------------------ |
| `delegate`        | depends on you   | you define them       | a named, domain-specific method type |
| `Func<...>`       | ✅ yes           | up to 16 inputs       | a method that computes and returns   |
| `Action<...>`     | ❌ no (`void`)   | up to 16 inputs       | a method that just does something    |
| `EventHandler<T>` | n/a              | `(sender, eventArgs)` | publish–subscribe between objects    |

## things to remember

1. A **delegate** is a "method as a value" you can pass it, store it, call it later.
2. **`Func`** returns something, **`Action`** returns nothing. That's the only difference.
3. **Events** are delegates for publish–subscribe: `+=` to subscribe, `-=` to unsubscribe.
4. All of this = **flexible, pluggable behaviour** the caller decides what happens.
