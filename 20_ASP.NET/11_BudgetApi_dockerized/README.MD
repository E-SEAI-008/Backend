# Budget API – Commands & Compose 🐳

### User secrets

```bash
dotnet user-secrets init
dotnet user-secrets list
dotnet user-secrets set "Jwt:Key" "45e68f228c874d424fc7d9dae6db56475c7778278333195e51fe9ac3121e0f46"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=YOUR_NEON_HOST"
```

### .dockerignore

```text
**/bin/
**/obj/
.git/
**/TestResults/
docker.env
**/.env
**/.env.*
```

### docker.env

```dotenv
ConnectionStrings__DefaultConnection=YOUR_NEON_CONNECTION_STRING
Jwt__Key=YOUR_LONG_JWT_SECRET_KEY
ASPNETCORE_ENVIRONMENT=Development
```

> Add `docker.env` to `.gitignore`

### Dockerfile

```dockerfile
# Stage 1: Build our API
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY BudgetApi.csproj .
RUN dotnet restore BudgetApi.csproj
COPY . .
RUN dotnet publish BudgetApi.csproj -c Release -o /app/publish

# Stage 2: Run our API
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BudgetApi.dll"]
```

### Docker build & run

```bash
docker build -t budgetapi:docker101 .
docker run --name budgetapiC --env-file docker.env -p 8080:8080 budgetapi:docker101
```

### Docker Compose – Neon

`compose.yaml`:

```yaml
services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    image: budgetapi:docker101
    ports:
      - '8080:8080'
    env_file:
      - docker.env
```

```bash
docker compose config
docker compose up --build
docker compose down
```

> Stop the previous `docker run` container first to free port 8080.

### Optional: Docker Compose – PostgreSQL + Volume

For a **new, empty** PostgreSQL database, make sure a complete EF Core migration exists (with `CreateTable(...)`). The Budget API also needs the optional startup `MigrateAsync()`, enabled by `ApplyMigrations=true`. **Do not run `dotnet ef database update` against Neon by accident.**

Replace `compose.yaml` with:

```yaml
services:
  api:
    build:
      context: .
      dockerfile: dockerfile
    image: budgetapi:docker101
    ports:
      - '8080:8080'
    env_file:
      - docker.env
    environment:
      ConnectionStrings__DefaultConnection: 'Host=db;Port=5432;Database=budgetdb;Username=postgres;Password=localdev123'
      ASPNETCORE_ENVIRONMENT: Development
      ApplyMigrations: 'true'
    depends_on:
      db:
        condition: service_healthy

  db:
    image: postgres:18
    environment:
      POSTGRES_DB: budgetdb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: localdev123
    volumes:
      - postgres_data:/var/lib/postgresql
    healthcheck:
      test: ['CMD-SHELL', 'pg_isready -U postgres -d budgetdb']
      interval: 5s
      timeout: 3s
      retries: 10

volumes:
  postgres_data:
```

```bash
docker compose up --build
docker compose down
```

> `Host=db` connects to PostgreSQL inside Compose. The named volume preserves database data; `docker compose down -v` deletes it. The example password is only for a local demo.
