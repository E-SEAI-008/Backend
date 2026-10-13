### ASP.NET Minimal APIs

- **ASP.NET** = the web framework on top of .NET (like Express on Node)
- `dotnet new web` scaffolds it; `app.Run()` starts the server that **keeps running**
- **Hosting** sets up the server, config, logging, and DI
- **DI** = register a service once, ask for it in endpoints, .NET injects it
- **Middleware** = a chain that every request flows through (order matters)
- **Routing** = matches method + path to a handler; params from URL, query, or body
- **Minimal APIs** = our lightweight, Express-style approach

### Node/Express <=> ASP.NET

| Express                     | ASP.NET Minimal API                  |
| --------------------------- | ------------------------------------ |
| `app.get("/", ...)`         | `app.MapGet("/", ...)`               |
| `app.post("/", ...)`        | `app.MapPost("/", ...)`              |
| `app.listen(...)`           | `app.Run()`                          |
| `(req, res, next) => {}`    | `(context, next) => {}`              |
| `req.body`                  | bound automatically to a typed param |
| `res.json({...})`           | `return new {...}` (auto JSON)       |
| `res.status(201).json(...)` | `Results.Created(loc, value)`        |
