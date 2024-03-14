using System.ComponentModel.DataAnnotations;

namespace Todos.Dtos;

public class UserCreatedDto
{
    public UserCreatedDto(string username, string password, string firstName, string lastName)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}