using System.ComponentModel.DataAnnotations;

namespace Todos.Dtos;

public class UserResponseDto
{
    public UserResponseDto(string username, string firstName, string lastName)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }

    [Required]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}