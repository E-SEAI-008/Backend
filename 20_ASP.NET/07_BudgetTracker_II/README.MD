# Budget API

| Method | Endpoint                                               | Purpose                             |
| ------ | ------------------------------------------------------ | ----------------------------------- |
| POST   | `/api/transactions`                                    | Add income or an expense            |
| GET    | `/api/transactions`                                    | List all transactions               |
| GET    | `/api/transactions/{id}`                               | Find one transaction                |
| PATCH  | `/api/transactions/{id}`                               | Update description, amount, or date |
| DELETE | `/api/transactions/{id}`                               | Remove a transaction                |
| GET    | `/api/reports/summary?start=YYYY-MM-DD&end=YYYY-MM-DD` | Return totals for a date range      |

### Install the packages

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Scalar.AspNetCore
```

| Package                               | Purpose                                   |
| ------------------------------------- | ----------------------------------------- |
| Microsoft.EntityFrameworkCore.Design  | EF Core migration tooling                 |
| Npgsql.EntityFrameworkCore.PostgreSQL | PostgreSQL database provider              |
| Microsoft.AspNetCore.OpenApi          | Generates API documentation               |
| Scalar.AspNetCore                     | Interactive API documentation and testing |

### Connection string

```bash
# Initialize once per project
dotnet user-secrets init

# Set or update your Neon connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"

# Show saved secrets — includes the password
dotnet user-secrets list

# Optional: remove the saved connection string => it would fallback to the connectionString in app.settings.json

# dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

### Create the database tables

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Test with Scalar

```bash
dotnet run
```

Open http://localhost:3000/scalar/v1.

Select an endpoint → **Test Request** → enter parameters or JSON → **Send**.

**POST /api/transactions**

```json
{
  "type": "Expense",
  "description": "Groceries",
  "amount": 50,
  "date": "2026-10-29"
}
```

**PATCH /api/transactions/{id}**

Send only the fields you want to change:

```json
{
  "amount": 60,
  "date": "2026-10-30"
}
```

**GET report**

```text
/api/reports/summary?start=2026-10-01&end=2026-10-31
```

**DELETE /api/transactions/{id}**

Expect **204 No Content**. GET the same ID again → **404 Not Found**
