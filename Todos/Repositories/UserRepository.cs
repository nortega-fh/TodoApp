using Microsoft.EntityFrameworkCore;
using Todos.DbContexts;
using Todos.Entities;

namespace Todos.Repositories;

public class UserRepository(TodoContext context) : IUserRepository
{
    public async Task<User?> Find(string username)
    {
        var user = await context.Users.Where(u => u.Username.Equals(username)).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User?> Find(long userId)
    {
        return await context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<User> Save(User user)
    {
        var savedUser = context.Add(user);
        await context.SaveChangesAsync();
        return savedUser.Entity;
    }

    public async Task<User> Update(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task Delete(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}