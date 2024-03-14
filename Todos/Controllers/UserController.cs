using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todos.Dtos;
using Todos.Services;

namespace Todos.Controllers;

[ApiController]
[Authorize]
[Route("/api/users")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPut("{userId:long}")]
    public async Task<ActionResult> UpdateUser(long userId, UserCreatedDto user)
    {
        var oldUser = await service.FindUser(userId);
        if (oldUser is null)
        {
            return NotFound();
        }
        oldUser.Username = user.Username;
        oldUser.Password = user.Password;
        oldUser.FirstName = user.FirstName;
        oldUser.LastName = user.LastName;
        await service.UpdateUser(oldUser);
        return NoContent();
    }

    [HttpDelete("{userId:long}")]
    public async Task<IActionResult> DeleteUser(long userId)
    {
        var user = await service.FindUser(userId);
        if (user is null)
        {
            return NotFound();
        }
        await service.DeleteUser(user);
        return NoContent();
    }
}