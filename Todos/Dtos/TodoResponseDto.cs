namespace Todos.Dtos;

public class TodoResponseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}