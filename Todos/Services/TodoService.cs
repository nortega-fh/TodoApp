using Todos.Entities;
using Todos.Repositories;

namespace Todos.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Todo>> GetAllTodosAsync(long userId)
    {
        return await _repository.GetAllTodosAsync(userId);
    }

    public async Task<Todo?> GetTodoByIdAsync(long todoId)
    {
        return await _repository.GetTodoByIdAsync(todoId);
    }

    public async Task<Todo?> CreateTodoAsync(Todo createdTodo)
    {
        return await _repository.SaveTodoAsync(createdTodo);
    }

    public async Task<Todo?> UpdateTodoAsync(long todoId, Todo updateTodo)
    {
        return await _repository.SaveTodoAsync(updateTodo);
    }

    public async Task<bool> TodoExistsAsync(long todoId)
    {
        return await GetTodoByIdAsync(todoId) is not null;
    }

    public async Task DeleteTodoAsync(long todoId)
    {
        var todo = await GetTodoByIdAsync(todoId);
        _repository.Delete(todo!);
    }
}