using System.ComponentModel.DataAnnotations;

namespace Todos.Dtos;

public class TodoCreateDto(string title)
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = title;
}