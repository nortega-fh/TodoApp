using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Todos.Dtos;
using Todos.Entities;
using Todos.Services;

namespace Todos.Controllers;

[ApiController]
[Authorize]
[Route("api/users/{userId:long}/todos")]
public class TodoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITodoService _todoService;
    private readonly IJwtTokenHandler _tokenHandler;

    public TodoController(ITodoService todoService, IMapper mapper, IJwtTokenHandler tokenHandler)
    {
        _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll([FromRoute] long userId)
    {
        if (!_tokenHandler.IsTokenValid(userId, User.Claims))
        {
            return Forbid();
        }
        return Ok(_mapper.Map<IEnumerable<TodoResponseDto>>(await _todoService.GetAllTodosAsync(userId)));
    }

    [HttpGet("{todoId:long}")]
    public async Task<ActionResult<Todo>> GetById([FromRoute] long userId, long todoId)
    {
        var todo = await _todoService.GetTodoByIdAsync(todoId);
        if (todo is null)
        {
            return NotFound();
        }
        if (todo.UserId != userId || !_tokenHandler.IsTokenValid(userId, User.Claims))
        {
            return Forbid();
        }
        return Ok(_mapper.Map<TodoResponseDto>(todo));
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo([FromRoute] long userId, TodoCreateDto createDto)
    {
        if (!_tokenHandler.IsTokenValid(userId, User.Claims))
        {
            return Forbid();
        }
        var todo = _mapper.Map<Todo>(createDto);
        todo.UserId = userId;
        return Created(Request.GetDisplayUrl(),
            _mapper.Map<TodoResponseDto>(await _todoService.CreateTodoAsync(todo)));
    }

    [HttpPut("{todoId:long}")]
    public async Task<ActionResult> UpdateTodo([FromRoute] long userId, long todoId, TodoUpdateDto updateDto)
    {
        var todo = await _todoService.GetTodoByIdAsync(todoId);
        if (todo is null)
        {
            return NotFound();
        }
        if (!_tokenHandler.IsTokenValid(userId, User.Claims) || todo.UserId != userId)
        {
            return Forbid();
        }
        var updatedTodo = _mapper.Map(updateDto, todo);
        await _todoService.UpdateTodoAsync(todoId, updatedTodo);
        return NoContent();
    }

    [HttpDelete("{todoId:long}")]
    public async Task<ActionResult> DeleteTodo([FromRoute] long userId, long todoId)
    {
        if (!_tokenHandler.IsTokenValid(userId, User.Claims))
        {
            return Forbid();
        }
        if (!await _todoService.TodoExistsAsync(todoId))
        {
            return NotFound();
        }
        await _todoService.DeleteTodoAsync(todoId);
        return NoContent();
    }
}