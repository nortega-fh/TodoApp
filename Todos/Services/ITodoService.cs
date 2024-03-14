using Todos.Entities;

namespace Todos.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllTodosAsync(long userId);
    Task<Todo?> GetTodoByIdAsync(long todoId);
    Task<Todo?> CreateTodoAsync(Todo todo);
    Task<Todo?> UpdateTodoAsync(long todoId, Todo todo);
    Task<bool> TodoExistsAsync(long todoId);
    Task DeleteTodoAsync(long todoId);
}