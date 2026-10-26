## QUick Ref

### 1. Install the packages

Run these commands in the project directory (where your `.csproj` file is located):

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

| Package                                | Why we use it                                         |
| -------------------------------------- | ----------------------------------------------------- |
| `Microsoft.EntityFrameworkCore`        | Core ORM features: entities, queries, change tracking |
| `Microsoft.EntityFrameworkCore.Sqlite` | SQLite database provider                              |
| `Microsoft.EntityFrameworkCore.Design` | Design-time support for migrations                    |

### 2. Install the EF CLI tool

The **NuGet packages** are project dependencies; **`dotnet-ef`** is a command-line tool for working with migrations.

```bash
# see globally installed tools
dotnet tool list --global

# install once (if not installed)
dotnet tool install --global dotnet-ef

# Or update an existing installation
dotnet tool update --global dotnet-ef

# Verify
dotnet ef --version
```

### 3. Create the DbContext

`DbContext` is the bridge between C# entities and the database. `DbSet<T>` exposes entities we can query and change.

**`Infrastructure/ApplicationDbContext.cs`**

```csharp
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
}
```

Our models have a **one-to-many relationship**: one `User` can have many `Post` objects; `Post.UserId` is the foreign key. EF Core can discover this relationship by convention.

### 4. Configure the database connection

**`appsettings.json`** — add this section to your existing configuration:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=blog.db"
}
```

**`Program.cs`** — register EF Core and the SQLite provider _before_ `builder.Build()`:

### 5. Create and update the database (migrations)

```bash
# generate the initial migration
dotnet ef migrations add InitialCreate

# apply pending migrations to SQLite (creates blog.db if needed)
dotnet ef database update

# view migrations
dotnet ef migrations list

# after changing an entity or OnModelCreating configuration
dotnet ef migrations add ConfigureUserEmail
dotnet ef database update

# remove the latest migration ONLY if it has not been applied
dotnet ef migrations remove
```

**Remember:** `migrations add` generates schema-change code; `database update` applies it. Neither command inserts your API's users or posts.

### Docs

- [EF Core overview](https://learn.microsoft.com/en-us/ef/core/)
- [DbContext configuration and lifetime](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
- [SQLite provider](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/)
- [Migrations overview](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [Relationships](https://learn.microsoft.com/en-us/ef/core/modeling/relationships/)
- [SQLite collations](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/collation)
