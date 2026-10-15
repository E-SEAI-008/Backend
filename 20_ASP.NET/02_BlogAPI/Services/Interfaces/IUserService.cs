using BlogApi.Models;

namespace BlogApi.Services.Interfaces;

public interface IUserService
{
    User? Get(Guid Id);         // one user by id, or null
    IReadOnlyList<User> List(); // GET all users
    User Create(string name, string email); // create a new user
    bool Delete(Guid Id);
}