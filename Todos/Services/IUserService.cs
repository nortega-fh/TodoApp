using Todos.Entities;

namespace Todos.Services;

public interface IUserService
{
    Task<User> CreateUser(User createdUser);
    Task<User?> FindUser(string username, string password);
    Task<User?> FindUser(long userId);
    Task<User?> UpdateUser(User userDetails);
    Task DeleteUser(User user);
    Task<bool> UserExists(long userId);
}