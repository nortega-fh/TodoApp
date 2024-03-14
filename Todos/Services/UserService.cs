using Microsoft.AspNetCore.Identity;
using Todos.Entities;
using Todos.Repositories;

namespace Todos.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<User> CreateUser(User createdUser)
    {
        createdUser.Password = new PasswordHasher<User>().HashPassword(createdUser, createdUser.Password);
        return await repository.Save(createdUser);
    }

    public async Task<User?> FindUser(string username, string password)
    {
        var user = await repository.Find(username);
        if (user is null)
        {
            return null;
        }
        var isPasswordEquals = new PasswordHasher<User>().VerifyHashedPassword(
                                   user, user.Password, password
                               )
                               == PasswordVerificationResult.Success;
        return isPasswordEquals ? user : null;
    }

    public async Task<User?> FindUser(long userId)
    {
        return await repository.Find(userId);
    }

    public async Task<User?> UpdateUser(User userDetails)
    {
        userDetails.Password = new PasswordHasher<User>().HashPassword(userDetails, userDetails.Password);
        return await repository.Update(userDetails);
    }

    public async Task DeleteUser(User user)
    {
        await repository.Delete(user);
    }

    public async Task<bool> UserExists(long userId)
    {
        return await repository.Find(userId) is not null;
    }
}