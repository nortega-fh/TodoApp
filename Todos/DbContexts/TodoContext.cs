using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todos.Entities;

namespace Todos.DbContexts;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany<Todo>()
            .WithOne(todo => todo.User)
            .HasForeignKey(todo => todo.UserId)
            .HasPrincipalKey(user => user.Id);

        var admin = new User("admin", "admin", "admin", "admin")
        {
            Id = 1
        };
        admin.Password = new PasswordHasher<User>().HashPassword(admin, "admin");
        modelBuilder.Entity<User>().HasData(admin);

        modelBuilder.Entity<Todo>().HasData(
            new Todo
            {
                Id = 1,
                Title = "Take out the trash",
                IsCompleted = false,
                UserId = 1
            },
            new Todo
            {
                Id = 2,
                Title = "Drive kids to school",
                IsCompleted = true,
                UserId = 1
            }
        );
    }
}