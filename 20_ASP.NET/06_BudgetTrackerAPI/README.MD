### BudgetTracker API

| Method   | Endpoint                                 | Purpose                                         |
| -------- | ---------------------------------------- | ----------------------------------------------- |
| `POST`   | `/transactions`                          | Add income or an expense                        |
| `GET`    | `/transactions`                          | List transactions, optionally filtered          |
| `GET`    | `/transactions/{id}`                     | Find one transaction                            |
| `PUT`    | `/transactions/{id}`                     | Update an existing transaction                  |
| `DELETE` | `/transactions/{id}`                     | Remove a transaction                            |
| `GET`    | `/reports?from=YYYY-MM-DD&to=YYYY-MM-DD` | Return transactions and totals for a date range |

### Install the packages

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Scalar.AspNetCore
# dotnet add package Microsoft.Extensions.Validation
```

| Package                                 | Why do we need it?                                                                   |
| --------------------------------------- | ------------------------------------------------------------------------------------ |
| `Microsoft.EntityFrameworkCore.Design`  | Helps EF tools create migrations and work with our context at design time            |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | Lets EF Core communicate with PostgreSQL; brings in the EF Core runtime dependencies |
| `Microsoft.AspNetCore.OpenApi`          | Generates an OpenAPI document describing our endpoints                               |
| `Scalar.AspNetCore`                     | Displays that document as interactive API documentation                              |

### Connection Strings

```bash
# Initialize User Secrets once per project
dotnet user-secrets init

# Set or update the connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"

# List saved secrets
dotnet user-secrets list

# Remove the connection string
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

> removing a secret does not remove the database. If the same key exists in `appsettings.json`, the app will fall back to that value.

---

### Test the API

Run the app with `dotnet run` and check the localhost port in the terminal.

- **Scalar:** Open `http://localhost:3000/scalar/v1`, select an endpoint, click **Test Request**, then **Send**.

Example POST `/transactions`:

```json
{
  "type": "Expense",
  "description": "Groceries",
  "amount": 50,
  "date": "2026-10-29"
}
```

Copy the returned `id` to test GET, PUT and DELETE `/transactions/{id}`. PUT uses the same body fields as POST.

Try filters and reports:

```text
GET /transactions?type=Expense
GET /reports?from=2026-10-01&to=2026-10-31
GET /reports?from=2026-10-01&to=2026-10-31
GET /reports?from=2026-10-01&to=2026-10-15+&type=Income
GET /reports?from=2026-10-01&to=2026-10-15+&type=Expense
```
