namespace Lab3.DbFirst.Models;

public partial class Event
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public Guid ProfessorId { get; set; }
    public Guid GroupId { get; set; }
    public EventType Type { get; set; }
    public string Name { get; set; } = null!;
    public int DayOfWeek { get; set; }

    public virtual Group Group { get; set; } = null!;
    public virtual User Professor { get; set; } = null!;
}

public enum EventType
{
    Lab,
    Lecture,
    Practice
}