## Quick comparison table

| Interface                   | Iterate | Count | Add / Remove | Intent                                   |
| --------------------------- | :-----: | :---: | :----------: | ---------------------------------------- |
| `IEnumerable<T>`            |    ✓    |   ✗   |      ✗       | Go through items                         |
| `ICollection<T>`            |    ✓    |   ✓   |      ✓       | Work with a collection and its items     |
| `IReadOnlyCollection<T>`    |    ✓    |   ✓   |      ✗       | Expose items and their count for reading |
| `IList<T>`                  |    ✓    |   ✓   |      ✓       | Access items by index                    |
| `IDictionary<TKey, TValue>` |    ✓    |   ✓   |      ✓       | Access values by key                     |

## Docs

- [System.Collections.Generic — collection interfaces overview](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-10.0)
- [IEnumerable<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
- [ICollection<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)
- [IReadOnlyCollection<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
- [IList<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)
- [IDictionary<TKey, TValue>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)
