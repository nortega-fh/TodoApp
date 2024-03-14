using Todos.Entities;

namespace Todos.Repositories;

public interface ITodoRepository
{
    Task<IQueryable<Todo>> GetAllTodosAsync(long userId);
    Task<Todo?> GetTodoByIdAsync(long todoId);
    Task<Todo?> SaveTodoAsync(Todo todo);
    void Delete(Todo todo);
}