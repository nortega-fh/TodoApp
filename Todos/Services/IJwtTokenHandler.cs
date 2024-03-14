using System.Security.Claims;
using Todos.Models;

namespace Todos.Services;

public interface IJwtTokenHandler
{
    bool IsTokenValid(long expectedUserId, IEnumerable<Claim> claims);
    Task<JwtToken> GenerateToken(string username, string password);
}