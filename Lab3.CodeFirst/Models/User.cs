using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.CodeFirst.Models;

public class User
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName => String.Join(" ", new string?[] {LastName, FirstName, MiddleName});
    public Position Position { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Group? Group { get; set; }
    [NotMapped] 
    public string? GroupName => Group?.Name;

}

public enum Position
{
    Professor,
    Assistant,
    Student,
    SystemAdministrator
}