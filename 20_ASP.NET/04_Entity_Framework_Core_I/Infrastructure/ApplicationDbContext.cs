using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();

    // we can do more!
}