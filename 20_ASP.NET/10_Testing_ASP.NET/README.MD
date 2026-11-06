```bash
# Create a solution
dotnet new sln -n BudgetApi --format sln

# Add our existing API project
dotnet sln BudgetApi.sln add BudgetApi/BudgetApi.csproj

# Create the xUnit test project
dotnet new xunit -n BudgetApi.Tests

# Connect the test project to our API
dotnet add BudgetApi.Tests/BudgetApi.Tests.csproj reference BudgetApi/BudgetApi.csproj

# Add the test project to the solution
dotnet sln BudgetApi.sln add BudgetApi.Tests/BudgetApi.Tests.csproj

# Add the integration testing package
dotnet add BudgetApi.Tests/BudgetApi.Tests.csproj package Microsoft.AspNetCore.Mvc.Testing

# Verify everything builds
dotnet build BudgetApi.sln

# Tests
dotnet test
dotnet test BudgetApi.Tests/BudgetApi.Tests.csproj
dotnet test BudgetApi.Tests/BudgetApi.Tests.csproj --filter "FullyQualifiedName~TestClassName"

# Metrics – install dotnet-counters (only once)
dotnet tool list --global
dotnet tool install --global dotnet-counters

# Start the API in a separate terminal
dotnet run --project BudgetApi

# Metrics – use PowerShell in a second terminal
dotnet-counters ps

# Monitor HTTP requests (replace PID with actual process ID)
dotnet-counters monitor --process-id PID --counters Microsoft.AspNetCore.Hosting

```
