using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Todos.Models;

namespace Todos.Services;

public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public JwtTokenHandler(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<JwtToken> GenerateToken(string username, string password)
    {
        var user = await _userService.FindUser(username, password);
        if (user is null)
        {
            return new JwtToken(string.Empty);
        }
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Auth:Secret"]
                                                                            ?? throw new InvalidOperationException()));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new("sub", user?.Id.ToString() ?? string.Empty),
            new("name", user?.FirstName ?? string.Empty),
            new("lastName", user?.LastName ?? string.Empty)
        };
        var token = new JwtSecurityToken(
            _configuration["Auth:Issuer"] ?? throw new InvalidOperationException(),
            _configuration["Auth:Audience"] ?? throw new InvalidOperationException(),
            claims,
            DateTime.Now,
            DateTime.Now.AddHours(2),
            signingCredentials
        );
        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public bool IsTokenValid(long expectedUserId, IEnumerable<Claim> claims)
    {
        var tokenHasId = long.TryParse(claims.FirstOrDefault(claim => claim.Type.Contains("nameidentifier"))?.Value,
            out var tokenUserId);
        return tokenHasId && expectedUserId == tokenUserId;
    }
}