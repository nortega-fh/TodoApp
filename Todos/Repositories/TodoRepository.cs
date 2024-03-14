using Microsoft.EntityFrameworkCore;
using Todos.DbContexts;
using Todos.Entities;

namespace Todos.Repositories;

public class TodoRepository(TodoContext todoContext) : ITodoRepository
{
    public async Task<IQueryable<Todo>> GetAllTodosAsync(long userId)
    {
        return (await todoContext.Todos.Where(todo => todo.UserId == userId).ToListAsync()).AsQueryable();
    }

    public async Task<Todo?> GetTodoByIdAsync(long todoId)
    {
        return await todoContext.Todos.Where(todo => todo.Id == todoId).FirstOrDefaultAsync();
    }

    public async Task<Todo?> SaveTodoAsync(Todo todo)
    {
        var existingTodo = await todoContext.Todos.Where(t => t.Id == todo.Id).FirstOrDefaultAsync();
        if (existingTodo is null)
        {
            var createdTodo = await todoContext.AddAsync(todo);
            existingTodo = createdTodo.Entity;
        }

        await todoContext.SaveChangesAsync();
        return existingTodo;
    }

    public void Delete(Todo todo)
    {
        todoContext.Remove(todo);
        todoContext.SaveChanges();
    }
}