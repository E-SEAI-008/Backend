### Quick reference

| Task                | Code                                                 |
| ------------------- | ---------------------------------------------------- |
| Write a file        | `File.WriteAllText(path, text)`                      |
| Read a file         | `File.ReadAllText(path)`                             |
| Append line by line | `using var w = new StreamWriter(path, append: true)` |
| Check a file exists | `File.Exists(path)`                                  |
| Build a path        | `Path.Combine(folder, file)`                         |
| Create a folder     | `Directory.CreateDirectory(folder)`                  |
| Object → JSON       | `JsonSerializer.Serialize(obj)`                      |
| JSON → object       | `JsonSerializer.Deserialize<T>(json)`                |
| Pretty JSON         | `new JsonSerializerOptions { WriteIndented = true }` |

### to remember

1. Use `using` with streams so files close automatically.
2. Always `try` / `catch` around file and JSON operations — outside data fails.
3. `Deserialize<T>` needs the type — that's how it knows what to rebuild.
