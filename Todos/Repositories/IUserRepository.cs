using Todos.Entities;

namespace Todos.Repositories;

public interface IUserRepository
{
    Task<User?> Find(string username);
    Task<User?> Find(long userId);
    Task<User> Save(User user);
    Task<User> Update(User user);
    Task Delete(User user);
}