# Blog API: First steps

```bash
dotnet restore
dotnet ef database update
dotnet run
```

The database will be created using the existing migrations.

In Development mode, the application also seeds a sample user and post if they don't already exist.

## API Endpoints

### Users

| Method | Endpoint      | Description         |
| ------ | ------------- | ------------------- |
| GET    | `/users`      | Get all users       |
| GET    | `/users/{id}` | Get a user by ID    |
| POST   | `/users`      | Create a user       |
| PUT    | `/users/{id}` | Update user details |
| DELETE | `/users/{id}` | Delete a user       |

### Posts

| Method | Endpoint           | Description           |
| ------ | ------------------ | --------------------- |
| GET    | `/posts`           | Get all posts         |
| GET    | `/posts/{id}`      | Get a post by ID      |
| GET    | `/posts?search=EF` | Search posts by title |
| POST   | `/posts`           | Create a post         |
| PUT    | `/posts/{id}`      | Update post details   |
| DELETE | `/posts/{id}`      | Delete a post         |

## Example Requests

Create a user:

```json
{
  "name": "john",
  "email": "john@example.com",
  "bio": "Learning EF Core"
}
```

Create a post (use an existing user ID):

```json
{
  "userId": "YOUR-USER-GUID",
  "title": "Hello EF Core",
  "content": "My first blog post!"
}
```

Update only a post's title:

```http
PUT /posts/{id}
```

```json
{
  "title": "Updated Title"
}
```

Our update endpoints support partial updates, so you only need to send the fields you want to change.

## Useful

```bash
dotnet ef migrations list
dotnet ef migrations add MigrationName
dotnet ef database update
```

Create a new migration when you change the database model or its schema related configuration. Regular CRUD operations don't need migrations.
