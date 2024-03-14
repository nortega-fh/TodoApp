using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Entities;

public class User(string username, string password, string firstName, string lastName)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = username;

    [Required]
    [MaxLength(250)]
    public string Password { get; set; } = password;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = firstName;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = lastName;

    public ICollection<Todo> Todos { get; }
}