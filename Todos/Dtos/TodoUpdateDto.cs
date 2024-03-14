using System.ComponentModel.DataAnnotations;

namespace Todos.Dtos;

public class TodoUpdateDto(string title)
{
    [Required] public string Title { get; set; } = title;

    [Required] public bool IsCompleted { get; set; }
}