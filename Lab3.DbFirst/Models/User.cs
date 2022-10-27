namespace Lab3.DbFirst.Models;

public partial class User
{
    public User()
    {
        Events = new HashSet<Event>();
    }

    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public Guid? GroupId { get; set; }
    public string Password { get; set; } = null!;
    public Position Position { get; set; }

    public virtual Group? Group { get; set; }
    public virtual ICollection<Event> Events { get; set; }
}

public enum Position
{
    Professor,
    Assistant,
    Student,
    SystemAdministrator
}