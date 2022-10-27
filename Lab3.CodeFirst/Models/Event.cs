namespace Lab3.CodeFirst.Models;

public class Event
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public User Professor { get; set; }
    public Group Group { get; set; }
    public EventType Type { get; set; }
    public string Name { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}

public enum EventType
{
    Lab, 
    Lecture,
    Practice
}