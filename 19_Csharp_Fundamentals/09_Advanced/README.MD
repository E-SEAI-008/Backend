## value vs reference, all together

| Type              | Value / Reference | Equality        | Use for                                  |
| ----------------- | ----------------- | --------------- | ---------------------------------------- |
| `enum`            | value             | —               | a fixed set of options (days, states)    |
| `struct`          | value             | value-based     | small, lightweight data                  |
| `readonly struct` | value             | value-based     | immutable value data (safety, speed)     |
| `record struct`   | value             | value-based     | immutable data + concise syntax + `with` |
| `class`           | **reference**     | reference-based | complex objects with state + behaviour   |
| `record`          | **reference**     | value-based     | data-centric models, DTOs, immutability  |
| `interface`       | —                 | —               | contracts, abstraction, polymorphism     |

## to remember

1. **Value type** = copied. **Reference type** = shared. The whole foundation.
2. **enum** = named options. **struct** = lightweight value data.
3. **class** = shared object, reference equality. **record** = value equality + `with`.
4. **interface** = a contract many types can fulfil.
5. **`with`** makes a modified copy — works on records and record structs.
