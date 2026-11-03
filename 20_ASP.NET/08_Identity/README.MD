# Budget API

| Method | Endpoint                                           | Purpose                                           |
| ------ | -------------------------------------------------- | ------------------------------------------------- |
| POST   | `/auth/register`                                   | Register a new user                               |
| POST   | `/auth/login`                                      | Login and receive a JWT                           |
| GET    | `/auth/me`                                         | Get the authenticated user's information          |
| POST   | `/transactions`                                    | Add income or an expense                          |
| GET    | `/transactions`                                    | List the current user's transactions              |
| GET    | `/transactions/{id}`                               | Find one of the user's transactions               |
| PATCH  | `/transactions/{id}`                               | Update description, amount, or date               |
| DELETE | `/transactions/{id}`                               | Remove a transaction                              |
| GET    | `/reports/summary?start=YYYY-MM-DD&end=YYYY-MM-DD` | Return the current user's totals for a date range |

**Authentication:** All transaction and report endpoints require a valid JWT.

### Install the packages

```bash
# ASP.NET Core Identity + JWT
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

| Package                                           | Purpose                                          |
| ------------------------------------------------- | ------------------------------------------------ |
| Microsoft.EntityFrameworkCore.Design              | EF Core migration tooling                        |
| Npgsql.EntityFrameworkCore.PostgreSQL             | PostgreSQL database provider                     |
| Microsoft.AspNetCore.OpenApi                      | Generates OpenAPI documentation                  |
| Scalar.AspNetCore                                 | Interactive API documentation and testing        |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | Identity user management and EF Core integration |
| Microsoft.AspNetCore.Authentication.JwtBearer     | JWT authentication and token validation          |

### Connection string + JWT secret

We use **.NET User Secrets** to store sensitive configuration values during development.

```bash
# Initialize once per project
dotnet user-secrets init

# Neon PostgreSQL connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"

# JWT secret key (development only)
dotnet user-secrets set "Jwt:Key" "BudgetApiDevelopmentSecretKey2026AtLeast32Characters!"

# Show saved secrets — includes sensitive values!
dotnet user-secrets list
```

In `appsettings.json`, add these JWT settings alongside the existing configuration:

```json
"Jwt": {
  "Issuer": "BudgetApi",
  "Audience": "BudgetApi",
  "ExpiryMinutes": 60
}
```

**Important:** The JWT secret key is used to sign and validate tokens. We store it in User Secrets instead of committing it to `appsettings.json`.

adn is for local development only.

### Create or update the database tables

After adding Identity to an existing project, create and apply a new migration:

```bash
dotnet ef migrations add AddIdentity
dotnet ef database update
```

This adds the ASP.NET Core Identity tables, including `AspNetUsers`, `AspNetRoles`, and related tables.

### Test with Scalar

```bash
dotnet run
```

Open http://localhost:3000/scalar/v1.

Select an endpoint → **Test Request** → enter parameters or JSON → **Send**.

**1. Register a user**

**2. Login**

`POST /auth/login`

Use the same email and password. Copy the returned JWT.

**3. Authenticate requests**

In Scalar, provide the JWT using Bearer authentication so protected requests send:

```http
Authorization: Bearer <token>
```

**4. Create a transaction**

`POST /transactions`

```json
{
  "type": "Expense",
  "description": "Groceries",
  "amount": 50,
  "date": "2026-11-03"
}
```

**5. Update a transaction**

`PATCH /transactions/{id}`

Send only the fields you want to change:

```json
{
  "amount": 60,
  "date": "2026-10-30"
}
```

**6. Get the report**

```text
/reports/summary?start=2026-10-01&end=2026-10-31
```

**7. Delete a transaction**

`DELETE /transactions/{id}`

Expect **204 No Content**. GET the same ID again → **404 Not Found**.

Without a valid JWT, protected endpoints return **401 Unauthorized**.

Bearer
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJCdWRnZXRBcGkiLCJpc3MiOiJCdWRnZXRBcGkiLCJleHAiOjE3OTM3MDUwNjYsInN1YiI6IjAxYTFhMGVmLWQxMWYtNzZiYi1hZjFjLWZmMjZmNDk0OTJhMiIsImlhdCI6MTc5MzcwMTQ2NiwibmJmIjoxNzkzNzAxNDY2fQ.hAzqibk9ErpRdboXoBqaE9TmbmPDBvFpCjb_ZMSgJeY

USER 2
Bearer

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJCdWRnZXRBcGkiLCJpc3MiOiJCdWRnZXRBcGkiLCJleHAiOjE3OTM3MDUyODEsInN1YiI6IjAxYTFhMGYzLTU5YWYtNzEzNC05ZWM4LWExMzlmYjE2ZTdlNiIsImlhdCI6MTc5MzcwMTY4MSwibmJmIjoxNzkzNzAxNjgxfQ.pRNJNguWDzRa_nUvVnW4jsQgAt-5gGuG-Vu0ZGnEUSc
